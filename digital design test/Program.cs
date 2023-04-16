using System;
using System.Reflection.Metadata.Ecma335;


class PointTest
{
    public string word;
    public int ValueByKey;
    public string s;
    
}

class Program
{
    static void Clean()
    {
        int flag = 0;
        string news = "";
        string path = "tolstoj_lew_nikolaewich-text_0040.fb2";
        StreamReader t = new StreamReader(path);
        StreamWriter f = new StreamWriter("newfile.txt", true);
  
        while (!t.EndOfStream)
        {
            var s = t.ReadLine();
            if (s != null)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    char c = s[i];
                    if (c == '<')
                    {
                        flag = 1;     
                    }
                    if (flag != 1)
                    {
                        news = news + c;
                    }
                    if (flag == 1)
                    {
                        if (c == '>')
                        {
                            flag = 0;
                        }
                    }
                }

            }

            f.WriteLine(news);
            news = "";
            flag = 0;
        }
        t.Close();
        f.Close();
    }

    static void Main()
    {
        Clean();
        var dict = new Dictionary<string, int>();
        var p = new PointTest();
        string path = "newfile.txt";
        StreamReader t = new StreamReader(path);
        while (!t.EndOfStream)
        {
            var s = t.ReadLine();
            if (s != null)
            {
                char[] separator = new char[] { ' ' };
                string[] subs = s.Split(separator, StringSplitOptions.RemoveEmptyEntries);


                foreach (var sub in subs) // Проход по полученным словам с целью записи в словрь
                {
                    char[] signs = { '.', '<', '>', '!', '?', ',', ';', ':', '"', '*', '(', ')', '-', '[', ']' };
                    string str = sub.Trim(signs);


                    p.word = str.ToLower(); // Приводим все слова в один регистр


                    if (dict.ContainsKey(p.word)) // Если слово есть в словаре, то прибавляем количество его использования
                    {
                        dict.TryGetValue(p.word, out p.ValueByKey);
                        dict[p.word] = p.ValueByKey + 1;
                    }
                    else // Добавляем слово в словарь, если его там нет
                    {
                        dict.Add(p.word, 1);
                    }

                }

            }
        }
        t.Close();  // Закрываем файл 
        dict.Remove("-");
        dict.Remove("--");
        dict.Remove(" ");

        dict = dict.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

        //Запись в файл Output.txt

        StreamWriter f = new StreamWriter("Output.txt");
        foreach (var word in dict)
        {
            f.WriteLine($"{word.Key}         {word.Value}");
        }
        f.Close(); // Закрываем файл
        Console.WriteLine("Done");

    }
   
}
