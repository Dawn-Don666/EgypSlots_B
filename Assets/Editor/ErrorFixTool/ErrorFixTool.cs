using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Unity混淆报错修复工具（一键获取控制台报错）
/// 功能：一键读取Unity Console中的 报错，自动双向替换名称
/// </summary>
public class ErrorFixTool : EditorWindow
{
    /// <summary>
    /// 替换记录信息
    /// </summary>
    private class ReplaceRecord
    {
        public string FilePath { get; set; }           // 文件路径
        public string ReplaceType { get; set; }        // 替换类型：正向/反向
        public string OldName { get; set; }            // 替换前
        public string NewName { get; set; }            // 替换后
        public List<int> LineNumbers { get; set; }     // 行号列表
        public string ReplaceForm { get; set; }        // 替换形式：泛型/普通
        public int Count { get; set; }                 // 替换次数
    }
    // 排除目录（路径包含以下字符串的目录会被跳过）
    static List<string> excludeDirectories = new List<string>
    {
        "\\Resources\\Assets",
        "\\Resources\\Prefab\\Bubbles",
        "\\Resources\\Art\\Tex\\UI\\Item",
        "\\Resources\\Art\\Tex\\UI\\UI_Bubble",
        "Art\\Tex\\UI\\Bubble\\Items",
        "Script\\Editor",
        "Assets\\Script\\LvSystem",
        "\\Editor\\",  // 排除Editor目录（工具自身所在目录）
    };

    // 排除文件（路径包含以下字符串的文件会被跳过）
    static string[] excludeFiles = new string[]
    {
        "ObjectPool.cs", "ServerData.cs", "GameData.cs", "DataConfig.cs",
        "GameConfig", "GameUtil.cs", "CheckoutTask.cs", "ShopJson.cs",
        "LevelPropertyEditor.cs", "LevelPropertyEditor.uss", "LevelPropertyEditor.uxml",
        "LevelSystemEditor.cs", "LevelTitleEditor.cs", "LevelTitleEditor.uss",
        "LevelTitleEditor.uxml", "Tab1SystemEditor.cs", "BubbleTypeAsset.cs",
        "LevelItem.cs", "LevelSystem.cs",
    };

    private static string scripteEncoding = "utf-8"; // 脚本文件编码
    private static Dictionary<string, string> wordDic = new Dictionary<string, string>(); // 正向映射：旧名称→新名称
    private static Dictionary<string, string> reverseWordDic = new Dictionary<string, string>(); // 反向映射：新名称→旧名称
    private string consoleErrorLog = ""; // 控制台报错内容
    private string replaceLog = ""; // 替换操作日志
    private Vector2 scrollPosition = Vector2.zero; // 滚动条位置

    /// <summary>
    /// 编辑器菜单入口
    /// </summary>
    [MenuItem("Tools/修复混淆报错（一键获取控制台）")]
    public static void OpenWindow()
    {
        ErrorFixTool window = GetWindow<ErrorFixTool>("混淆报错修复工具");
        window.minSize = new Vector2(600, 400);
        window.Show();
    }

    /// <summary>
    /// 窗口启用时初始化
    /// </summary>
    private void OnEnable()
    {
        // 初始化名称映射字典（WordDic.csv 在 Editor 根目录）
        InitWordDic(Path.Combine(Application.dataPath, "Editor/WordDic.csv"));
    }

    /// <summary>
    /// 绘制窗口GUI（简化为两个核心按钮）
    /// </summary>
    private void OnGUI()
    {
        GUILayout.Label("=== Unity混淆报错修复工具 ===", EditorStyles.boldLabel);
        GUILayout.Space(10);

        // 1. 一键获取控制台报错按钮
        if (GUILayout.Button("🔍 一键获取控制台报错", GUILayout.Height(40)))
        {
            consoleErrorLog = GetConsoleErrorLogs();
            replaceLog = ""; // 清空历史替换日志
            // 显示获取结果
            if (string.IsNullOrEmpty(consoleErrorLog))
            {
                replaceLog = "⚠️ 控制台中未检测到CS1061/CS0246/CS0103/CS0117类型的报错！";
            }
            else
            {
                replaceLog = $"✅ 成功获取到控制台报错：\n{consoleErrorLog}";
            }
        }

        GUILayout.Space(10);

        // 2. 自动修复按钮（仅当获取到报错时可点击）
        GUI.enabled = !string.IsNullOrEmpty(consoleErrorLog);
        if (GUILayout.Button("🛠️ 解析报错并自动修复", GUILayout.Height(40)))
        {
            FixErrorNames();
            EditorUtility.DisplayDialog("修复完成",
                "报错修复操作已执行，详见下方日志。\n\n" +
                "提示：如果还有新的错误出现，请点击【刷新窗口】按钮重置状态，然后重新获取报错。",
                "确定");
        }
        GUI.enabled = true;

        GUILayout.Space(5);

        // 3. 刷新按钮（重置窗口状态，准备处理下一批错误）
        if (GUILayout.Button("🔄 刷新窗口（重置状态）", GUILayout.Height(35)))
        {
            consoleErrorLog = "";
            replaceLog = "";
            scrollPosition = Vector2.zero;
            replaceLog = "✅ 窗口已刷新，可以重新获取报错进行修复";
        }

        GUILayout.Space(10);

        // 4. 操作日志区域（带滚动条）
        GUILayout.Label("操作日志：");
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(250));
        EditorGUILayout.TextArea(replaceLog, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
        EditorGUILayout.EndScrollView();
    }

    #region 核心方法
    /// <summary>
    /// 初始化双向名称映射字典（从WordDic.csv读取）
    /// </summary>
    /// <param name="csvPath">CSV文件路径</param>
    private static void InitWordDic(string csvPath)
    {
        wordDic.Clear();
        reverseWordDic.Clear();

        // 检查CSV文件是否存在
        if (!File.Exists(csvPath))
        {
            Debug.LogError($"【ErrorFixTool】WordDic.csv不存在：{csvPath}");
            return;
        }

        // 读取CSV文件内容（自动释放资源）
        string content;
        try
        {
            using (StreamReader sr = new StreamReader(csvPath, Encoding.GetEncoding(scripteEncoding)))
            {
                content = sr.ReadToEnd();
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"【ErrorFixTool】读取WordDic.csv失败：{e.Message}");
            return;
        }

        // 解析CSV行（按行分割，忽略空行）
        string[] lines = content.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string line in lines)
        {
            string[] kv = line.Split(',');
            // 确保行数据包含至少两个有效字段
            if (kv.Length >= 2 && !string.IsNullOrEmpty(kv[0]) && !string.IsNullOrEmpty(kv[1]))
            {
                string oldName = kv[0].Trim();
                string newName = kv[1].Trim();

                // 正向字典：旧名称→新名称（避免重复键）
                if (!wordDic.ContainsKey(oldName))
                {
                    wordDic.Add(oldName, newName);
                }

                // 反向字典：新名称→旧名称（避免重复键）
                if (!reverseWordDic.ContainsKey(newName))
                {
                    reverseWordDic.Add(newName, oldName);
                }
            }
        }

        Debug.Log($"【ErrorFixTool】初始化双向字典完成：正向{wordDic.Count}条，反向{reverseWordDic.Count}条");
    }

    /// <summary>
    /// 直接读取Unity Console面板中的CS1061/CS0246/CS0103/CS0117报错（核心新功能）
    /// </summary>
    /// <returns>控制台中的目标报错内容</returns>
    private string GetConsoleErrorLogs()
    {
        StringBuilder errorLogs = new StringBuilder();
        string logPath = "";
        // 新增：用于存储不重复的报错行
        HashSet<string> uniqueErrorLines = new HashSet<string>();

        // （原有自动匹配日志路径的逻辑不变）
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            string oldPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Unity/Editor/Editor.log");
            string newPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Unity/Editor/Editor.log");
            logPath = File.Exists(newPath) ? newPath : oldPath;
        }
        else if (Application.platform == RuntimePlatform.OSXEditor)
        {
            logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "Library/Logs/Unity/Editor.log");
        }

        if (!File.Exists(logPath))
        {
            replaceLog = $"日志文件不存在，路径：{logPath}";
            return "";
        }

        try
        {
            // 强制刷新Unity日志（通过重新打开文件）
            System.Threading.Thread.Sleep(100);

            using (FileStream fs = new FileStream(logPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
            {
                string line;
                Queue<string> lastLines = new Queue<string>(5000); // 增加到5000行
                int totalLines = 0;
                int errorLinesFound = 0;

                while ((line = sr.ReadLine()) != null)
                {
                    totalLines++;
                    if (lastLines.Count >= 5000) lastLines.Dequeue();
                    lastLines.Enqueue(line);

                    // 实时检查是否包含错误（支持多种格式）
                    // Unity日志格式可能是：error CS0246: 或 CS0246: 或 (行,列): error CS0246:
                    if ((line.Contains("error CS1061:") || line.Contains("CS1061:")) ||
                        (line.Contains("error CS0246:") || line.Contains("CS0246:")) ||
                        (line.Contains("error CS0103:") || line.Contains("CS0103:")) ||
                        (line.Contains("error CS0117:") || line.Contains("CS0117:")))
                    {
                        // 确保是真正的错误行（排除一些误匹配）
                        if (line.Contains(".cs(") || line.Contains("error CS") || line.Contains("CS0246") || line.Contains("CS1061") || line.Contains("CS0103") || line.Contains("CS0117"))
                        {
                            errorLinesFound++;
                            // 只添加未出现过的报错行
                            if (uniqueErrorLines.Add(line))
                            {
                                errorLogs.AppendLine(line);
                            }
                        }
                    }
                }

                // 如果从最后5000行没找到，尝试从所有行中查找
                if (errorLogs.Length == 0 && totalLines > 5000)
                {
                    // 重新读取整个文件
                    fs.Seek(0, SeekOrigin.Begin);
                    sr.DiscardBufferedData();

                    while ((line = sr.ReadLine()) != null)
                    {
                        if ((line.Contains("error CS1061:") || line.Contains("CS1061:")) ||
                            (line.Contains("error CS0246:") || line.Contains("CS0246:")) ||
                            (line.Contains("error CS0103:") || line.Contains("CS0103:")) ||
                            (line.Contains("error CS0117:") || line.Contains("CS0117:")))
                        {
                            // 确保是真正的错误行
                            if (line.Contains(".cs(") || line.Contains("error CS") || line.Contains("CS0246") || line.Contains("CS1061") || line.Contains("CS0103") || line.Contains("CS0117"))
                            {
                                if (uniqueErrorLines.Add(line))
                                {
                                    errorLogs.AppendLine(line);
                                }
                            }
                        }
                    }
                }

                replaceLog = $"日志路径：{logPath}\n";
                replaceLog += $"读取总行数：{totalLines}\n";
                replaceLog += $"检测到错误行数：{errorLinesFound}\n";

                // 如果没有找到错误，显示最后几行日志用于调试
                if (errorLogs.Length == 0 && lastLines.Count > 0)
                {
                    replaceLog += $"\n⚠️ 未找到错误，显示日志最后10行用于调试：\n";
                    var last10Lines = lastLines.Skip(Math.Max(0, lastLines.Count - 10)).ToList();
                    foreach (var lastLine in last10Lines)
                    {
                        replaceLog += $"  {lastLine}\n";
                    }
                }
            }
        }
        catch (Exception e)
        {
            replaceLog = $"读取日志失败：{e.Message}，路径：{logPath}";
            return "";
        }

        return errorLogs.ToString().Trim();
    }
    /// <summary>
    /// 解析控制台报错，执行双向名称替换
    /// </summary>
    private void FixErrorNames()
    {
        // 校验报错日志
        if (string.IsNullOrEmpty(consoleErrorLog))
        {
            replaceLog = "无控制台报错可解析";
            return;
        }

        // 从报错信息中提取文件路径和错误名称的对应关系
        Dictionary<string, List<string>> fileErrorMap = ExtractFileErrorMap(consoleErrorLog);
        if (fileErrorMap.Count == 0)
        {
            replaceLog = "未从控制台报错中提取到文件信息";
            return;
        }

        replaceLog += $"📌 找到{fileErrorMap.Count}个报错文件需要修复：\n";
        foreach (var kvp in fileErrorMap)
        {
            replaceLog += $"  - {kvp.Key}：{string.Join(", ", kvp.Value)}\n";
        }
        replaceLog += "\n";

        // 检查字典映射情况
        HashSet<string> allErrorNames = new HashSet<string>();
        foreach (var errors in fileErrorMap.Values)
        {
            foreach (var errorName in errors)
            {
                allErrorNames.Add(errorName);
            }
        }

        replaceLog += $"📋 字典检查：\n";
        foreach (string errorName in allErrorNames)
        {
            bool hasForward = wordDic.ContainsKey(errorName);
            bool hasReverse = reverseWordDic.ContainsKey(errorName);
            if (hasForward)
            {
                replaceLog += $"  ✅ {errorName} → 正向字典中找到：{wordDic[errorName]}\n";
            }
            else if (hasReverse)
            {
                replaceLog += $"  ✅ {errorName} → 反向字典中找到：{reverseWordDic[errorName]}\n";
            }
            else
            {
                replaceLog += $"  ❌ {errorName} → 字典中未找到映射！\n";
            }
        }
        replaceLog += "\n";

        // 只处理报错中提到的文件
        int totalReplaceCount = 0;
        int forwardReplaceCount = 0;  // 正向替换次数
        int reverseReplaceCount = 0;  // 反向替换次数
        List<ReplaceRecord> replaceRecords = new List<ReplaceRecord>(); // 记录所有替换信息

        foreach (var kvp in fileErrorMap)
        {
            string relativeFilePath = kvp.Key; // 如 "Assets\CashOut\CashOutManager.cs"
            List<string> errorNames = kvp.Value; // 该文件中的错误名称列表

            // 转换为完整路径
            string fullFilePath = Path.Combine(Application.dataPath.Replace("Assets", ""), relativeFilePath);
            fullFilePath = fullFilePath.Replace('/', '\\');

            if (!File.Exists(fullFilePath))
            {
                replaceLog += $"⚠️ 文件不存在：{relativeFilePath}\n";
                continue;
            }

            int fileReplaceCount = 0;
            string fileContent;

            // 读取文件内容
            try
            {
                using (StreamReader sr = new StreamReader(fullFilePath, Encoding.GetEncoding(scripteEncoding)))
                {
                    fileContent = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                replaceLog += $"⚠️ 读取文件失败：{relativeFilePath} | {e.Message}\n";
                continue;
            }

            if (string.IsNullOrEmpty(fileContent)) continue;

            replaceLog += $"📝 处理文件：{relativeFilePath}\n";

            // 保存原始文件内容用于计算行号
            string originalFileContent = fileContent;

            // 双向匹配替换
            foreach (string errorName in errorNames)
            {
                string targetOldName = "";
                string targetNewName = "";
                string replaceType = "";
                bool isMatched = false;

                // 正向匹配：报错名称是旧名称 → 替换为新名称
                if (wordDic.TryGetValue(errorName, out string newName))
                {
                    targetOldName = errorName;
                    targetNewName = newName;
                    replaceType = "正向替换（键→值）";
                    isMatched = true;
                    replaceLog += $"  🔍 {errorName} → 正向匹配：{newName}\n";
                }
                // 反向匹配：报错名称是新名称 → 替换为旧名称
                else if (reverseWordDic.TryGetValue(errorName, out string oldName))
                {
                    targetOldName = oldName;
                    targetNewName = errorName;
                    replaceType = "反向替换（值→键）";
                    isMatched = true;
                    replaceLog += $"  🔍 {errorName} → 反向匹配：{oldName}\n";
                }
                else
                {
                    replaceLog += $"  ⚠️ {errorName} → 字典中未找到映射\n";
                    continue; // 跳过这个名称，继续下一个
                }

                // 执行正则替换（匹配独立单词，避免部分匹配）
                // 支持泛型类型：匹配 "TypeName<" 或 "TypeName<>" 或独立的 "TypeName"
                if (isMatched)
                {
                    // 先尝试匹配泛型形式（如 MonoSingleton< 或 MonoSingleton<>）
                    string genericPattern = Regex.Escape(targetOldName) + @"\s*<";
                    Regex genericRegex = new Regex(genericPattern);
                    MatchCollection genericMatches = genericRegex.Matches(originalFileContent);

                    if (genericMatches.Count > 0)
                    {
                        // 计算行号
                        List<int> lineNumbers = GetLineNumbers(originalFileContent, genericMatches);

                        // 记录替换信息
                        ReplaceRecord record = new ReplaceRecord
                        {
                            FilePath = relativeFilePath,
                            ReplaceType = replaceType,
                            OldName = targetOldName + "<",
                            NewName = targetNewName + "<",
                            LineNumbers = lineNumbers,
                            ReplaceForm = "泛型形式",
                            Count = genericMatches.Count
                        };
                        replaceRecords.Add(record);

                        // 替换泛型形式：MonoSingleton< 替换为 FlawAdventure<
                        fileContent = genericRegex.Replace(fileContent, targetNewName + "<");
                        fileReplaceCount += genericMatches.Count;
                        totalReplaceCount += genericMatches.Count;
                        if (replaceType.Contains("正向"))
                            forwardReplaceCount += genericMatches.Count;
                        else
                            reverseReplaceCount += genericMatches.Count;

                        replaceLog += $"  ✅ 泛型替换：{targetOldName}< → {targetNewName}<，共{genericMatches.Count}处\n";
                    }
                    else
                    {
                        replaceLog += $"  ℹ️ 未找到泛型形式：{targetOldName}<\n";
                    }

                    // 再尝试匹配非泛型形式（独立单词）
                    // 注意：这里会匹配文件中所有出现的地方，即使报错信息中只提到一次
                    string regStr = @"\W" + Regex.Escape(targetOldName) + @"\W";
                    Regex regex = new Regex(regStr);
                    MatchCollection matches = regex.Matches(originalFileContent);

                    if (matches.Count > 0)
                    {
                        // 计算行号
                        List<int> lineNumbers = GetLineNumbers(originalFileContent, matches);

                        // 记录替换信息
                        ReplaceRecord record = new ReplaceRecord
                        {
                            FilePath = relativeFilePath,
                            ReplaceType = replaceType,
                            OldName = targetOldName,
                            NewName = targetNewName,
                            LineNumbers = lineNumbers,
                            ReplaceForm = "普通形式",
                            Count = matches.Count
                        };
                        replaceRecords.Add(record);

                        // 使用Replace方法一次性替换所有匹配（更高效，且能处理同一文件中多个相同错误）
                        fileContent = regex.Replace(fileContent, match =>
                        {
                            string matchStr = match.Value;
                            return matchStr.Replace(targetOldName, targetNewName);
                        });
                        fileReplaceCount += matches.Count;
                        totalReplaceCount += matches.Count;
                        if (replaceType.Contains("正向"))
                            forwardReplaceCount += matches.Count;
                        else
                            reverseReplaceCount += matches.Count;

                        replaceLog += $"  ✅ 普通替换：{targetOldName} → {targetNewName}，共{matches.Count}处（已修复文件中所有出现）\n";
                    }
                    else
                    {
                        replaceLog += $"  ℹ️ 未找到普通形式：{targetOldName}\n";
                    }
                }
            }

            // 写入替换后的内容（仅当文件有替换时写入）
            if (fileReplaceCount > 0)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(fullFilePath, false, Encoding.GetEncoding(scripteEncoding)))
                    {
                        sw.Write(fileContent);
                    }
                    replaceLog += $"  💾 文件已保存，共替换{fileReplaceCount}处\n";
                }
                catch (Exception e)
                {
                    replaceLog += $"  ⚠️ 写入文件失败：{e.Message}\n";
                }
            }
            else
            {
                replaceLog += $"  ℹ️ 该文件无需修改\n";
            }
            replaceLog += "\n";
        }

        // 刷新Unity资源数据库
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        replaceLog += $"\n📊 修复完成！总计替换{totalReplaceCount}处，涉及{fileErrorMap.Count}个文件";

        // 写入日志文件
        if (replaceRecords.Count > 0)
        {
            WriteLogFile(replaceRecords, fileErrorMap.Count, totalReplaceCount, forwardReplaceCount, reverseReplaceCount);
        }
    }

    /// <summary>
    /// 计算匹配位置所在的行号
    /// </summary>
    private List<int> GetLineNumbers(string content, MatchCollection matches)
    {
        List<int> lineNumbers = new List<int>();

        foreach (Match match in matches)
        {
            int position = match.Index;
            int lineNumber = 1;

            // 计算位置之前的换行符数量
            for (int i = 0; i < position && i < content.Length; i++)
            {
                if (content[i] == '\n')
                {
                    lineNumber++;
                }
                // 处理 \r\n 的情况，避免重复计数
                else if (content[i] == '\r' && (i + 1 >= content.Length || content[i + 1] != '\n'))
                {
                    lineNumber++;
                }
            }

            if (!lineNumbers.Contains(lineNumber))
            {
                lineNumbers.Add(lineNumber);
            }
        }

        lineNumbers.Sort();
        return lineNumbers;
    }

    /// <summary>
    /// 写入日志文件
    /// </summary>
    private void WriteLogFile(List<ReplaceRecord> records, int fileCount, int totalReplaceCount, int forwardCount, int reverseCount)
    {
        try
        {
            // 日志文件路径
            string logDir = Path.Combine(Application.dataPath, "Editor/ErrorFixTool");
            if (!Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }
            string logFilePath = Path.Combine(logDir, "ErrFixLog.txt");

            // 构建日志内容
            StringBuilder logContent = new StringBuilder();
            string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            logContent.AppendLine("========================================");
            logContent.AppendLine($"修复时间：{currentTime}");
            logContent.AppendLine("========================================");
            logContent.AppendLine();

            // 按文件分组记录
            var groupedRecords = records.GroupBy(r => r.FilePath);
            foreach (var group in groupedRecords)
            {
                logContent.AppendLine($"【文件】{group.Key}");

                foreach (var record in group)
                {
                    logContent.AppendLine($"  ├─ 替换类型：{record.ReplaceType}");
                    logContent.AppendLine($"  ├─ 替换：{record.OldName} → {record.NewName}");

                    // 行号信息
                    if (record.LineNumbers.Count > 0)
                    {
                        if (record.LineNumbers.Count <= 5)
                        {
                            logContent.AppendLine($"  ├─ 位置：第{string.Join("行、第", record.LineNumbers)}行（{record.ReplaceForm}）");
                        }
                        else
                        {
                            logContent.AppendLine($"  ├─ 位置：第{string.Join("行、第", record.LineNumbers.Take(5))}行...等{record.LineNumbers.Count}处（{record.ReplaceForm}）");
                        }
                    }

                    logContent.AppendLine($"  ├─ 替换前：{record.OldName}");
                    logContent.AppendLine($"  └─ 替换后：{record.NewName}");
                    logContent.AppendLine();
                }
            }

            logContent.AppendLine("========================================");
            logContent.AppendLine("本次修复统计：");
            logContent.AppendLine($"- 修复文件数：{fileCount}");
            logContent.AppendLine($"- 修复名称数：{records.Select(r => r.OldName).Distinct().Count()}");
            logContent.AppendLine($"- 总替换次数：{totalReplaceCount}");
            logContent.AppendLine($"- 正向替换：{forwardCount}次");
            logContent.AppendLine($"- 反向替换：{reverseCount}次");
            logContent.AppendLine("========================================");
            logContent.AppendLine();
            logContent.AppendLine();

            // 追加写入日志文件
            File.AppendAllText(logFilePath, logContent.ToString(), Encoding.GetEncoding(scripteEncoding));

            replaceLog += $"\n📝 日志已保存：{logFilePath}";
        }
        catch (Exception e)
        {
            replaceLog += $"\n⚠️ 写入日志失败：{e.Message}";
        }
    }

    /// <summary>
    /// 从报错日志中提取文件路径和错误名称的对应关系
    /// </summary>
    /// <param name="logText">报错日志文本</param>
    /// <returns>文件路径 -> 错误名称列表的字典</returns>
    private Dictionary<string, List<string>> ExtractFileErrorMap(string logText)
    {
        Dictionary<string, List<string>> fileErrorMap = new Dictionary<string, List<string>>();
        string[] lines = logText.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        // 匹配文件路径和错误名称
        // 格式：Assets\CashOut\CashOutManager.cs(14,31): error CS0246: The type or namespace name 'MonoSingleton<>' could not be found
        Regex fileErrorRegex = new Regex(@"Assets[^\(]+\.cs");

        // // 英文错误正则
        // // CS1061: 方法/属性不存在 - "for 'MethodName'" / "for 'MethodName<>'"
        // Regex cs1061Regex = new Regex(@"for '(?<name>\w+(?:<.*>)?)'");
        // // CS0246: 类型/命名空间不存在
        // Regex cs0246Regex1 = new Regex(@"name '(?<name>\w+(?:<.*>)?)' could not be found");
        // Regex cs0246Regex2 = new Regex(@"The type or namespace name '(?<name>\w+(?:<.*>)?)' could not be found");
        // // CS0103: 名称不存在 - "The name 'SaveDataManager' does not exist in the current context"
        // Regex cs0103Regex = new Regex(@"The name '(?<name>\w+(?:<.*>)?)' does not exist in the current context");
        // // CS0117: 成员不存在 - "'EBubbleType' does not contain a definition for 'RemoveBubble'"
        // Regex cs0117Regex = new Regex(@"does not contain a definition for '(?<name>\w+(?:<.*>)?)'");


        // 中文错误正则
        // CS1061：“XX”未包含“YY”的定义，且找不到可接受第一个“XX”类型参数的可访问扩展方法“YY”(是否缺少 using 指令或程序集引用?)
        Regex cs1061Regex = new Regex(@"“[^”]+”未包含“(?<name>\w+(?:<.*>)?)”的定义");
        // CS0246：找不到类型或命名空间名“XX”
        Regex cs0246Regex1 = new Regex(@"找不到类型或命名空间名“(?<name>\w+(?:<.*>)?)”");
        // CS0246：未能找到类型或命名空间名“XX”
        Regex cs0246Regex2 = new Regex(@"未能找到类型或命名空间名“(?<name>\w+(?:<.*>)?)”");
        // CS0103：当前上下文中不存在名称“XX”
        Regex cs0103Regex = new Regex(@"当前上下文中不存在名称“(?<name>\w+(?:<.*>)?)”");
        // CS0117：“XX”不包含“YY”的定义
        Regex cs0117Regex = new Regex(@"“[^”]+”不包含“(?<name>\w+(?:<.*>)?)”的定义");


        foreach (string line in lines)
        {
            // 提取文件路径
            Match fileMatch = fileErrorRegex.Match(line);
            if (!fileMatch.Success) continue;

            string filePath = fileMatch.Value;

            // 提取错误名称
            Match nameMatch = cs1061Regex.Match(line);
            if (!nameMatch.Success)
            {
                nameMatch = cs0246Regex1.Match(line);
            }
            if (!nameMatch.Success)
            {
                nameMatch = cs0246Regex2.Match(line);
            }
            if (!nameMatch.Success)
            {
                nameMatch = cs0103Regex.Match(line);
            }
            if (!nameMatch.Success)
            {
                nameMatch = cs0117Regex.Match(line);
            }

            if (nameMatch.Success)
            {
                string name = nameMatch.Groups["name"].Value;
                if (!string.IsNullOrEmpty(name))
                {
                    // 如果提取到的名称包含<>（如 MonoSingleton<>），去掉<>只保留类型名
                    if (name.Contains("<>"))
                    {
                        name = name.Replace("<>", "");
                    }

                    // 添加到字典
                    if (!fileErrorMap.ContainsKey(filePath))
                    {
                        fileErrorMap[filePath] = new List<string>();
                    }
                    if (!fileErrorMap[filePath].Contains(name))
                    {
                        fileErrorMap[filePath].Add(name);
                    }
                }
            }
        }

        return fileErrorMap;
    }

    /// <summary>
    /// 从报错日志中提取缺失的方法名/类名（保留此方法用于兼容，但主要使用ExtractFileErrorMap）
    /// </summary>
    /// <param name="logText">报错日志文本</param>
    /// <returns>提取到的名称列表</returns>
    private List<string> ExtractErrorNames(string logText)
    {
        // 用HashSet自动去重
        HashSet<string> errorNamesSet = new HashSet<string>();
        string[] lines = logText.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        // CS1061: 方法/属性不存在 - "for 'MethodName'"
        // 支持泛型：先匹配带 <> 的，再匹配不带 <> 的
        Regex cs1061Regex = new Regex(@"for '(?<name>\w+(?:<>)?)'");
        // CS0246: 类型不存在 - 支持多种格式：
        // 1. "name 'TypeName' could not be found" 或 "name 'TypeName<>' could not be found"
        // 2. "The type or namespace name 'TypeName' could not be found" 或 "The type or namespace name 'TypeName<>' could not be found"
        // 改进：使用 (?:\w+<>|\w+) 明确匹配带 <> 或不带 <> 的类型名
        Regex cs0246Regex1 = new Regex(@"name '(?<name>\w+<>|\w+)' could not be found");
        Regex cs0246Regex2 = new Regex(@"The type or namespace name '(?<name>\w+<>|\w+)' could not be found");
        // CS0103: 名称不存在 - "The name 'Name' does not exist in the current context"
        Regex cs0103Regex = new Regex(@"The name '(?<name>\w+(?:<>)?)' does not exist in the current context");

        foreach (string line in lines)
        {
            Match match = cs1061Regex.Match(line);
            if (!match.Success)
            {
                match = cs0246Regex1.Match(line);
            }
            if (!match.Success)
            {
                match = cs0246Regex2.Match(line);
            }
            if (!match.Success)
            {
                match = cs0103Regex.Match(line);
            }

            if (match.Success)
            {
                string name = match.Groups["name"].Value;
                if (!string.IsNullOrEmpty(name))
                {
                    // 如果提取到的名称包含<>（如 MonoSingleton<>），去掉<>只保留类型名用于匹配
                    // 因为实际代码中使用的是 MonoSingleton<Type>，而不是 MonoSingleton<>
                    if (name.Contains("<>"))
                    {
                        string nameWithoutGeneric = name.Replace("<>", "");
                        if (!string.IsNullOrEmpty(nameWithoutGeneric))
                        {
                            errorNamesSet.Add(nameWithoutGeneric);
                        }
                    }
                    else
                    {
                        errorNamesSet.Add(name);
                    }
                }
            }
        }

        // 转成List返回
        return errorNamesSet.ToList();
    }

    /// <summary>
    /// 递归获取所有CS文件（排除指定目录/文件）
    /// </summary>
    /// <param name="dirPath">根目录路径</param>
    /// <param name="fileList">输出文件列表</param>
    private void GetAllScriptFiles(string dirPath, ref List<string> fileList)
    {
        if (!Directory.Exists(dirPath)) return;

        DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
        foreach (FileSystemInfo fsInfo in dirInfo.GetFileSystemInfos())
        {
            string fullPath = fsInfo.FullName;

            // 跳过排除目录
            if (fsInfo is DirectoryInfo)
            {
                if (!excludeDirectories.Any(ex => fullPath.Contains(ex)))
                {
                    GetAllScriptFiles(fullPath, ref fileList);
                }
                continue;
            }

            // 筛选CS文件并跳过排除文件
            if (fsInfo is FileInfo fileInfo && fileInfo.Extension.Equals(".cs", StringComparison.OrdinalIgnoreCase))
            {
                if (!excludeFiles.Any(ex => fullPath.Contains(ex)))
                {
                    fileList.Add(fullPath);
                }
            }
        }
    }
    #endregion
}