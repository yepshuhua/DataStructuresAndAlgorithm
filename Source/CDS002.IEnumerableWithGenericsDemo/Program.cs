using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CDS002.IEnumerableWithGenericsDemo
{
    /// <summary>
    /// 对一个普通的文本文件，检索指定的关键词在文件中的数量
    /// 练习使用迭代子 Iterator 以及其中的处理性能
    /// </summary>
    class Program
    {
        static void Main()
        {
            Console.WriteLine("文件名：tempFile.txt");
            Console.Write("关键词:");
            var keyString = Console.ReadLine();
            TestReadingFile(keyString);
            Console.WriteLine("---");
            TestStreamReaderEnumerable(keyString);

            Console.ReadKey();
        }

        /// <summary>
        /// 不使用自定义的迭代子检索指定的文本文件中，包含指定字符串的个数的方法
        /// </summary>
        /// <param name="keyString"></param>
        public static void TestReadingFile(string keyString)
        {
            var memoryBefore = GC.GetTotalMemory(true);
            StreamReader sr;
            try
            {
                sr = File.OpenText("C://Users//Administrator//source//repos//yepshuhua//DataStructuresAndAlgorithm//Document//tempFile.txt");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine(@"这个例子需要一个名为 tempFile.txt 的文件。");
                return;
            }
            var fileContents = new List<string>(); // 将文本内容添加到一个 List<string> 变量
            while (!sr.EndOfStream)
            {
                fileContents.Add(sr.ReadLine());
            }

            // 检索目标文本（字符串）
            var stringsFound =
                from line in fileContents
                where line.Contains(keyString)
                select line;

            sr.Close();
           // Console.WriteLine("数量：" + stringsFound.Count());

            //var memoryAfter = GC.GetTotalMemory(false); // 检查不使用迭代子并将结果输出到控制台之后的内存用量.
            //Console.WriteLine("不使用 Iterator 的内存用量 = \t" + string.Format(((memoryAfter - memoryBefore) / 1000).ToString(), "n") + "kb");
        }

        /// <summary>
        /// 使用迭代子方式检索指定的文本文件中，包含指定字符串的个数的方法
        /// </summary>
        /// <param name="keyString"></param>
        public static void TestStreamReaderEnumerable(string keyString)
        {
            var memoryBefore = GC.GetTotalMemory(true); // 检查使用迭代子之前的内存用量
            IEnumerable<String> stringsFound;
            // 使用 StreamReaderEnumerable 打开一个示例文件，检索对应的字符串
            try
            {
                stringsFound =
                      from line in new StreamReaderEnumerable(@"C://Users//Administrator//source//repos//yepshuhua//DataStructuresAndAlgorithm//Document//tempFile.txt")
                      where line.Contains(keyString)
                      select line;
                int count = 0;//循环行数
                int count1 = 0;//计数个数
                int count2 = 0;//计数行数
                int oo = 0;//接keyString的位置
                int bb = 0;//计算上一个keyString与现今keyString相隔

                foreach (var items in stringsFound)
                {
                    count += 1;
                    //Console.WriteLine(items);
                }

                foreach (var item in stringsFound)
                {
                    count1++;
                    int cc = 0;//计算keyString开始位置
                    int yy = 0; //接第一个index为-1的数，为一行里最后一个keyString服务
                    for (int i = 0; i < count; i++)
                    {
                        int index = item.IndexOf(keyString, cc);
                        //计算keyString13个字符里是否有keyString后输出keyString后面的字符
                        if (index == -1)
                            yy++;
                        if (index != -1)
                        {
                            if (i == 0) { oo = index; }//第一位
                            else//中间数
                            {
                                bb = index - oo - 4;
                                oo = index;
                            }
                        }
                        else
                        {
                            bb = item.Count() - cc;
                            //去除多余，只取一次
                            if (yy == 1)//最后位
                            {
                                if (bb > 9)
                                    Console.WriteLine(item.Substring(cc, 9) + "”;");
                                else
                                    Console.WriteLine(item.Substring(cc, bb) + "”;");
                            }
                            continue;
                        }
                        if (cc != 0)//为第一与中间查询的服务
                        {
                            if (bb < 9)
                                Console.WriteLine(item.Substring(cc, bb) + "”;");
                            else
                            { Console.WriteLine(item.Substring(cc, 9) + "”;"); }
                        }

                        //计算keyString的位置
                        if (cc < item.Count() && index != -1)
                        {
                            count2++;
                            string x = item.Substring(index, keyString.Length);
                            Console.Write("{0}.第{1}行,第{2}个字母开始:“{3}", count2, count1, index + 1, x);

                            cc = index + keyString.Length;
                        }
                    }

                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine(@"这个例子需要一个名为 tempFile.txt 的文件。");
                return;
            }

           // var memoryAfter = GC.GetTotalMemory(false); // 检查使用迭代子并将结果输出到控制台之后的内存用量。
           // Console.WriteLine("使用 Iterator 的内存用量 = \t" + string.Format(((memoryAfter - memoryBefore) / 1000).ToString(), "n") + "kb");
        }


    }

    /// <summary>
    /// 一个定制的实现IEnumerable<T> 的类，为此，还需要实现相应的
    ///  IEnumerable 和 IEnumerator<T>
    /// </summary>
    public class StreamReaderEnumerable : IEnumerable<string>
    {
        private string _filePath;
        public StreamReaderEnumerable(string filePath)
        {
            _filePath = filePath;
        }

        // 必须实现 GetEnumerator，用于返回一个新的 StreamReaderEnumerator.
        public IEnumerator<string> GetEnumerator() => new StreamReaderEnumerator(_filePath);

        // 同时必须实现 IEnumerable.GetEnumerator，但当成一个私有方法实现
        private IEnumerator GetEnumerator1() => this.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator1();

        /// <summary>
        /// 在实现 IEnumerable<T> 时，必须实现IEnumerator<T>，范例代码中，遍历文件内容时，每一行一次
        /// 实现 IEnumerable<T> 还需要实现 IEnumerator 和析构函数 IDisposable
        /// </summary>
        public class StreamReaderEnumerator : IEnumerator<string>
        {
            private StreamReader _sr;
            public StreamReaderEnumerator(string filePath)
            {
                _sr = new StreamReader(filePath);
            }
            private string _current;
            // 实现 IEnumerator<T>().Current 公开属性，但实现所必须的 IEnumerator.Current 则为私有属性.
            public string Current
            {
                get
                {
                    if (_sr == null || _current == null)
                        throw new InvalidOperationException();
                    return _current;
                }
            }
            private object Current1 => this.Current;
            object IEnumerator.Current => Current1;
            // 实现 IEnumerator 所必须的 MoveNext 和 Reset。
            public bool MoveNext()
            {
                _current = _sr.ReadLine();
                if (_current == null)
                    return false;
                return true;
            }
            public void Reset()
            {
                _sr.DiscardBufferedData();
                _sr.BaseStream.Seek(0, SeekOrigin.Begin);
                _current = null;
            }
            // 实现析构函数，必须的。
            private bool disposedValue = false;
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposedValue)
                {
                    if (disposing) { } // 析构所需要的资源
                    _current = null;
                    if (_sr != null)
                    {
                        _sr.Close();
                        _sr.Dispose();
                    }
                }
                this.disposedValue = true;
            }
            ~StreamReaderEnumerator() { Dispose(false); }
        }
    }
}