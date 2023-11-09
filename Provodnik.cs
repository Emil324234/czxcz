class Provodnik
{
    static void Main(string[] args)
    {
       
        DriveInfo[] drives = DriveInfo.GetDrives();
        for (int i = 0; i < drives.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {drives[i].Name}");
        }

        Console.WriteLine("Выберите диск:");
        int selectedDriveIndex = int.Parse(Console.ReadLine()) - 1;

      
        DriveInfo selectedDrive = drives[selectedDriveIndex];
        Console.WriteLine($"Имя диска: {selectedDrive.Name}");
        Console.WriteLine($"Всего памяти: {selectedDrive.TotalSize / (1024 * 1024 * 1024)} ГБ");
        Console.WriteLine($"Свободная память: {selectedDrive.AvailableFreeSpace / (1024 * 1024 * 1024)} ГБ");

     
        string currentPath = $"{selectedDrive.Name}\\";
        bool isMainMenu = true;
        bool exit = false;
        while (!exit)
        {
            Console.Clear();

            if (isMainMenu)
            {
                Console.WriteLine("Текущая папка: Меню выбора диска");
                Console.WriteLine();
                Console.WriteLine("Выберите диск:");
                int selectedOption = int.Parse(Console.ReadLine()) - 1;
                if (selectedOption >= 0 && selectedOption < drives.Length)
                {
                    selectedDrive = drives[selectedOption];
                    currentPath = $"{selectedDrive.Name}\\";
                    isMainMenu = false;
                }
            }
            else
            {
                Console.WriteLine("Текущая папка: " + currentPath);
                Console.WriteLine();

                string[] directories = Directory.GetDirectories(currentPath);
                string[] files = Directory.GetFiles(currentPath);

                Console.WriteLine("Папки:");
                for (int i = 0; i < directories.Length; i++)
                {
                    Console.WriteLine($"\t{i + 1}. {Path.GetFileName(directories[i])}");
                }

                Console.WriteLine("Файлы:");
                for (int i = 0; i < files.Length; i++)
                {
                    Console.WriteLine($"\t{i + 1}. {Path.GetFileName(files[i])}");
                }

                Console.WriteLine();
                Console.WriteLine("Выбери папку или файл: Escape для возврата ");
                string userInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    if (currentPath.Equals($"{selectedDrive.Name}\\"))
                    {
                        isMainMenu = true;
                    }
                    else
                    {
                        currentPath = Directory.GetParent(currentPath).FullName;
                    }
                }
                else if (int.TryParse(userInput, out int selectedOption))
                {
                    if (selectedOption >= 1 && selectedOption <= directories.Length)
                    {
                        currentPath = directories[selectedOption - 1];
                    }
                    else if (selectedOption > directories.Length && selectedOption <= directories.Length + files.Length)
                    {
                        string selectedFile = files[selectedOption - 1 - directories.Length];
                        Console.WriteLine("Запускаем файл: " + selectedFile);



                        Console.ReadLine();
                    }
                    else if (selectedOption == 0)
                    {

                        exit = true;
                    }
                }


                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    currentPath = Path.GetDirectoryName(currentPath);
                    if (!currentPath.EndsWith("\\"))
                    {
                        currentPath += "\\";
                    }
                }
            }
        }
    }
}
   

