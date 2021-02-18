using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesProcess
{
    public class DataReader
    {
        public List<Note> GetNotes()
        {
            var package = new ExcelPackage(new System.IO.FileInfo(@"C:\Workspace\Sukanya\tables.xlsx"));
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            ExcelWorksheet sheet = package.Workbook.Worksheets[0];

            var notes = new CustomExtensions().ConvertToList<Note>(sheet).Where(x => x.NoteId != 0).ToList();

            return notes;
        }

        public List<NoteASOC> GetNoteASOC()
        {
            var package = new ExcelPackage(new System.IO.FileInfo(@"C:\Workspace\Sukanya\tables.xlsx"));
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            ExcelWorksheet sheet = package.Workbook.Worksheets[1];

            var noteASOC = new CustomExtensions().ConvertToList<NoteASOC>(sheet).ToList();

            return noteASOC;
        }

        public List<NoteList> GetNoteList()
        {
            var package = new ExcelPackage(new System.IO.FileInfo(@"C:\Workspace\Sukanya\tables.xlsx"));
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            ExcelWorksheet sheet = package.Workbook.Worksheets[2];

            var noteList = new CustomExtensions().ConvertToList<NoteList>(sheet).Where(x => x.DataKey != "").ToList();

            return noteList;
        }

        public List<NoteSche> GetNotesche()
        {
            var package = new ExcelPackage(new System.IO.FileInfo(@"C:\Workspace\Sukanya\tables.xlsx"));
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            ExcelWorksheet sheet = package.Workbook.Worksheets[3];

            var GetNotesche = new CustomExtensions().ConvertToList<NoteSche>(sheet).Where(x => x.NoteKey != "").ToList();

            return GetNotesche;
        }

        public List<DisplayVariables> GetConfig()
        {
            var package = new ExcelPackage(new System.IO.FileInfo(@"C:\Workspace\Sukanya\config.xlsx"));
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            ExcelWorksheet sheet = package.Workbook.Worksheets[0];

            var GetConfigs = new CustomExtensions().ConvertToList<DisplayVariables>(sheet).ToList();

            return GetConfigs;
        }

        public List<SelectedData> GetSelectedData()
        {
            var package = new ExcelPackage(new System.IO.FileInfo(@"C:\Workspace\Sukanya\selectedData.xlsx"));
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            ExcelWorksheet sheet = package.Workbook.Worksheets[0];

            var selectedData = new CustomExtensions().ConvertToList<SelectedData>(sheet).ToList();

            return selectedData;
        }
    }
}
