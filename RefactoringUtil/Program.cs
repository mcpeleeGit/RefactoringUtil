using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactoringUtil
{
    class Program
    {
        static void Main(string[] args)
        {
            var r = new RefactoringMediator();

            var fm = new FileManager();
            var lc = new LargeClass(r);

            r.Set(fm, lc);
            r.Do();
            Console.ReadKey();
        }
    }
    public interface IFileManager
    {
    }
    public interface IRefactoring
    {
        void Checking();
    }
    public abstract class AbstractRefactoringUtil
    {
        public RefactoringMediator RefactoringMediator { get; set; }
        public AbstractRefactoringUtil(RefactoringMediator r)
        {
            RefactoringMediator = r;
        }
    }
    public class FileManager : abstractFileManager, IFileManager
    {
        public string RootPath { get; set; } = "";
        public void DisplayFilesLine(int overLines = 0)
        {
            if (!IsExistDirectory(RootPath)) return;
            RecursionDirectory(RootPath, overLines);
        }
    }

    public class abstractFileManager
    {
        protected bool IsExistDirectory(string dir)
        {
            return Directory.Exists(dir);
        }
        protected void RecursionDirectory(string path, int overLines)
        {
            var di = new DirectoryInfo(path);
            foreach (var item in di.GetDirectories())
            {
                RecursionDirectory(item.FullName, overLines);
            }
            foreach (var item in di.GetFiles())
            {
                string[] data = File.ReadAllLines(item.FullName);
                if (data.Length > overLines) Console.WriteLine(item.FullName + " : " + data.Length);
            }
        }
    }

    public class RefactoringMediator
    {
        private FileManager FileManager { get; set; }
        private IRefactoring Refactoring { get; set; }
        public void Set(FileManager fm, IRefactoring r)
        {
            FileManager = fm;
            Refactoring = r;
        }
        public void Do()
        {
            Refactoring.Checking();
        }
        public void DisplayFilesLine(string rootPath, int overLines = 0)
        {
            FileManager.RootPath = rootPath;
            FileManager.DisplayFilesLine(overLines);
        }
    }
    public class LargeClass : AbstractRefactoringUtil, IRefactoring
    {
        public LargeClass(RefactoringMediator r) : base(r)
        {
        }
        public void Checking()
        {
            RefactoringMediator.DisplayFilesLine(
                @"C:\DEV\WebSite\TqoonLibraries\AdprintLib\Service\",
                overLines: 500
                );
        }
    }


}
