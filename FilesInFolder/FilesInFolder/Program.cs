using System;
using System.IO;
using System.Collections.Generic;

namespace FilesInFolder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("**** Программа для переноса файлов ****");
            Console.WriteLine();

            string oldPath = " ";
            string newPath = " ";
            string select = " ";

            Menu();
            if (select == "0") return;

            do
            {
                Console.WriteLine("Введите полный путь папки исходной папки");
                oldPath = Console.ReadLine();
                oldPath = PathCheck(oldPath);
                if (select == "0") return;

                Console.WriteLine("Введите полный путь новой папки");
                newPath = Console.ReadLine();
                newPath=PathCheck(newPath);
                if (select == "0") return;

                if (select.Contains("1"))
                    {
                         FillFolders(oldPath);
                         Console.WriteLine("Перенос файлов в новую папку выполнен");
                    }
                if (select.Contains("2"))
                    {
                         FillFolders(oldPath);
                         Console.WriteLine("Копирование файлов в новую папку выполнено");
                    }
                Menu();
            }
            while (!select.Contains("0")) ;

            void FillFolders(string path)
            {
                string[] files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    Console.Write($"{file} - ");
                    string newfile = file.Substring(file.LastIndexOf("\\"));
                    newfile = newPath + newfile;
                    Console.WriteLine(newfile);
                    if (select.Contains("1"))
                    {
                        try { File.Move(file, newfile);}
                        catch { Console.WriteLine($"{newfile} уже существует "); }
                    }
                    if (select.Contains("2"))
                    {
                        try { File.Copy(file, newfile, false); }
                        catch {
                            Console.WriteLine($"{newfile} уже существует. Хотите его заменить - наберите 1, не хотите - наберите любой символ ");
                            if (Console.ReadLine() == "1") File.Copy(file, newfile, true);
                        }
                    }
                }

                string[] folders = Directory.GetDirectories(path);
                foreach (var folder in folders) FillFolders(folder);
            }

            void Menu()
            {
                Console.WriteLine();
                Console.WriteLine("Выберите действие - наберите соответствующую цифру");
                Console.WriteLine("1 - перемещать файлы из исходной папки и всех ее вложений в новую папку");
                Console.WriteLine("2 - копировать файлы из исходной папки и всех ее вложений в новую папку");
                Console.WriteLine("0 - выйти из программы");
                select = Console.ReadLine();
            }

            //Проверка пути
            string PathCheck(string path)
            {
                if (!Directory.Exists(path)) 
                {
                    Console.WriteLine("Путь указан неверно");
                    Console.WriteLine("Хотите выйти из программы? - Наберите 0. Хотите продолжить? - Наберите любой символ");
                    if (Console.ReadLine()!="0")
                    {
                        Console.WriteLine("Введите путь еще раз");
                        path = Console.ReadLine();
                        PathCheck(path);
                        return path;
                    }
                    else select = "0";
                }
                return path;
            }

        }
    }
}