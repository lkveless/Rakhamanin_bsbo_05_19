/* 
    Задания 1, 2 и 3 были сданы во время практики.
    Путь к файлу в заданиях #4 и #5 был взят с образца в методических материалах
*/


using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.IO.Compression;
using System.Xml.Linq;

namespace ConsoleApp1
{
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Company { get; set; }

    }
    class Program
    {
        static async Task Main(string[] args)
        {
            Boolean flag = true;
            while (flag)
            {
                Console.Write("Введите номер задания: (1/ 2/ 3/ 4/ 5/ 6 - для выхода из программы) ");
                string zadanie = Console.ReadLine();
                switch (zadanie)
                {
                    case "1":
                        DriveInfo[] drives = DriveInfo.GetDrives();
                        foreach (DriveInfo drive in drives)
                        {
                            Console.WriteLine($"Название: {drive.Name}");
                            Console.WriteLine($"Тип: {drive.DriveType}");
                            if (drive.IsReady)
                            {
                                Console.WriteLine($"Объемдиска: {drive.TotalSize}");
                                Console.WriteLine($"Свободноепространство: {drive.TotalFreeSpace}");
                                Console.WriteLine($"Метка: {drive.VolumeLabel}");
                            }
                            Console.WriteLine();
                        }
                        break;
                    case "2":
                        string path = @"D:\Documents\Рахманин\file123.txt";
                        FileInfo fileinf = new FileInfo(path);
                        if (!fileinf.Exists)
                        {
                            fileinf.Create();
                        }
                        Console.WriteLine("Файл создан. Вывод информации о файле на экран");
                        if (fileinf.Exists)
                        {
                            Console.WriteLine("Имя файла: {0}", fileinf.Name);
                            Console.WriteLine("Время создания: {0}", fileinf.CreationTime);
                            Console.WriteLine("Размер: {0}", fileinf.Length);
                        }
                        Console.WriteLine("Введите текст, который хотите записать в файл: ");
                        string textToFile = Console.ReadLine();
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
                            {
                                sw.WriteLine(textToFile);
                            }

                            Console.WriteLine("Запись выполнена");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        Console.WriteLine("Вывод текста из файла в консоль: ");
                        try
                        {
                            using (StreamReader sr = new StreamReader(path))
                            {
                                Console.WriteLine(sr.ReadToEnd());
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        Console.WriteLine("Подтвердите удаление файла: (yes/no)");
                        string agreement = Console.ReadLine();
                        switch (agreement)
                        {
                            case "yes":
                                if (fileinf.Exists)
                                {
                                    Console.WriteLine("Удаление файла");
                                    fileinf.Delete();
                                }
                                break;
                            case "no":
                                Console.WriteLine("Файл не будет удален, для дальнейшего удаления требуется заново выбрать второе задание.");
                                break;
                        }
                        break;
                    case "3":
                        using (FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate))
                        {
                            Person tom = new Person() { Name = "Tom", Age = 35 };
                            await JsonSerializer.SerializeAsync<Person>(fs, tom);
                            Console.WriteLine("Data has been saved to file");
                        }
                        using (FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate))
                        {
                            Person restoredPerson = await JsonSerializer.DeserializeAsync<Person>(fs);
                            Console.WriteLine($"Name: {restoredPerson.Name}  Age: {restoredPerson.Age}");
                        }
                        break;
                    case "4":
                                                
                            Console.WriteLine("Работа с XML файлом");
                            
                            string fileName = Directory.GetCurrentDirectory() + "base.xml";
                            
                            int trackId = 1;
                            XDocument doc = new XDocument(
                            new XElement("library",
                            new XElement("track",
                            new XAttribute("id", trackId++),
                            new XAttribute("genre", "Rap "),
                            new XAttribute("time", "1:46 "),
                            new XElement("name", "Boss "),
                            new XElement("artist", "Lil Pump "),
                            new XElement("album", "Lil Pump ")),
                            new XElement("track",
                            new XAttribute("id", trackId++),
                            new XAttribute("genre", "Rap "),
                            new XAttribute("time", "3:01 "),
                            new XElement("name", "Esskeetit "),
                            new XElement("artist", "Lil Pump "),
                            new XElement("album", "Harverd Dropout ")),
                            new XElement("track",
                            new XAttribute("id", trackId++),
                            new XAttribute("genre", "R&B "),
                            new XAttribute("time", "2:58 "),
                            new XElement("name", "7 rings "),
                            new XElement("artist", "Ariana Grande "),
                            new XElement("album", "Thank U, next ")),
                            new XElement("track",
                            new XAttribute("id", trackId++),
                            new XAttribute("genre", "Rap "),
                            new XAttribute("time", "3:16 "),
                            new XElement("name", "Famous "),
                            new XElement("artist", "Kanye West "),
                            new XElement("album", "The Life Of Pablo "))));

                            doc.Save(fileName);
                        Console.WriteLine("XML-файл создается в папке с проектом, по адресу: проект/bin/Debug/netcoreapp3.1");
                            Console.WriteLine("Введите цифру 1, чтоб вывести информацию о записях на экран");


                            string input = Console.ReadLine();
                            switch (input)
                            {
                                case "1":
                                    foreach (var item in doc.Root.Elements())
                                    {
                                        Console.WriteLine($"{item.Name}: {item.Value}");
                                    }
                                    break;
                            }
                        break;                                             

                    case "5":
                        string sourceFile = "D://test/book.pdf";
                        string compressedFile = "D://test/book.gz";
                        string targetFile = "D://test/book_new.pdf";

                        Compress(sourceFile, compressedFile);

                        Decompress(compressedFile, targetFile);
                

                        static void Compress(string sourceFile, string compressedFile)
                        {

                            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
                            {

                                using (FileStream targetStream = File.Create(compressedFile))
                                {

                                    using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                                    {
                                        sourceStream.CopyTo(compressionStream);
                                        Console.WriteLine("Сжатие файла {0} завершено. Исходный размер: {1}  сжатый размер: {2}.",
                                        sourceFile, sourceStream.Length.ToString(), targetStream.Length.ToString());
                                    }
                                }
                            }
                        }

                        static void Decompress(string compressedFile, string targetFile)
                        {

                            using (FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate))
                            {

                                using (FileStream targetStream = File.Create(targetFile))
                                {

                                    using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                                    {
                                        decompressionStream.CopyTo(targetStream);
                                        Console.WriteLine("Восстановлен файл: {0}", targetFile);
                                    }
                                }
                            }
                        }
                        break;
               

                    case "6":
                        flag = false;
                        break;
                }
            }
        }
    }
}