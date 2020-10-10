using System;
using System.IO;

class FileOperations {

  public static void Main() {
    FileManager newfile = new FileManager();

    // Create a new directory
    newfile.FilePath = @"files/file-folder"; // directory name
    newfile.CreateDirectory();

    // Write file in newly created directory
    newfile.FilePath = @"files/file-folder/another.txt"; // directory and file name
    newfile.CreateAndWriteFile();

    // Read from the newly created file
    newfile.FilePath = @"files/file-folder/another.txt";
    newfile.ReadFromFile();

    // Copy to new directory 
    string source = @"files/file-folder";
    string destination = @"files/another-folder";
    newfile.CopyToAnotherDirectory(source,destination,"another.txt");

    // Delete file
    newfile.FilePath =@"files/file-folder/another.txt";
    newfile.DeleteFile();

    // Delete directory
    newfile.FilePath =@"files/file-folder";
    newfile.DeleteDirectory();
  }

  class FileManager {
    private string _filePath;
    public string FilePath {
      get { return _filePath;}
      set { _filePath = value;}
    }

    public void CreateDirectory() {
      Directory.CreateDirectory(_filePath);
      Console.WriteLine("Creating directory successful");
    }

    // Function that creates file 
    public void CreateAndWriteFile() {
      if ( !File.Exists(_filePath) ) {
        // Create a file to write to.
        using (StreamWriter sw = File.CreateText(_filePath))
        {
          // Multiple lines written in the file using WriteLine function 
            sw.WriteLine("Hello");
            sw.WriteLine("Mousam Dhakal");
        }

        Console.WriteLine("Writing to the file successsful");
      } else {
        Console.WriteLine("File already exists, skipping file creating and writing.");
      }
    }

    public void ReadFromFile() {
      if(File.Exists(_filePath)) {
        // Open the file
        using (StreamReader sr = File.OpenText(_filePath))
        {   
            Console.WriteLine("");
            Console.WriteLine("File read, Contents of file:");
            string s;
            // As long as there are lines to read, read and display in console
            while ((s = sr.ReadLine()) != null)
            {
                Console.WriteLine(s);
            }
        }
      } else {
        // If file does not exist
        Console.WriteLine("File not found");
      }
    }

    public void CopyToAnotherDirectory(string sourcefolder, string destinationFolder, string fileName){
      Directory.CreateDirectory(destinationFolder);
      string sourceFile = Path.Combine(sourcefolder,fileName);
      string destinationFile = Path.Combine(destinationFolder,fileName);

      // Copy the file and overwrite if already exists
      File.Copy(sourceFile,destinationFile,true);
      Console.WriteLine();
      Console.WriteLine("Copy successful");
    }

    public void DeleteFile() {
      if(File.Exists(_filePath)){
        // Delete a file using try catch , as error is thrown trying to delete file already opened in another process
        try {
          File.Delete(_filePath);
          Console.WriteLine("Delete file successful");
        } 
        catch(IOException error) {
          Console.WriteLine(error.Message);
          return;
        }
      } else {
        Console.WriteLine("Could not find file to delete");
      }
    }

     public void DeleteDirectory() {
      if(Directory.Exists(_filePath)){
        // Delete a folder using try catch , as error is thrown trying to delete folder already opened in another process
        try {
          Directory.Delete(_filePath, true);
          Console.WriteLine("Delete directory successful");
        } 
        catch(IOException error) {
          Console.WriteLine(error.Message);
          return;
        }
      } else {
        Console.WriteLine("Could not find directory to delete");
      }
    }
  }

}
