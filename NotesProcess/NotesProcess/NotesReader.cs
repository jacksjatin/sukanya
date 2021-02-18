using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesProcess
{
    public class NotesReader
    {

        List<Note> _notes = new List<Note>();

        List<NoteASOC> _notesAsoc = new List<NoteASOC>();

        List<NoteList> _notesList = new List<NoteList>();

        List<NoteSche> _notesche = new List<NoteSche>();

        List<DisplayVariables> displayVariables = new List<DisplayVariables>();

        List<SelectedData> selectedData = new List<SelectedData>();

        DataReader dr = new DataReader();

        string model = "YPAL";

        public NotesReader()
        {


            _notes = dr.GetNotes().Where(x => x.Model == model).ToList();

            _notesAsoc = dr.GetNoteASOC().Where(x => x.Model == model).ToList();

            _notesList = dr.GetNoteList().Where(x => x.Model == model).ToList();

            _notesche = dr.GetNotesche().Where(x => x.Model == model).ToList();

            displayVariables = dr.GetConfig();

            selectedData = dr.GetSelectedData();
        }

        public void NotesFunc()
        {



            var noteItemsDict = new Dictionary<int, string>();





            //var displayVariables = new List<DisplayVariables>()
            //{
            //    new DisplayVariables(){Key ="PRTU.REFRIG",AssignedValue="R410A"},
            //    new DisplayVariables(){Key ="PRTU.BRAND",AssignedValue="JCI"},
            //};



            foreach (var value in displayVariables)
            {
                if (value != null && value.Key != null && value.AssignedValue != null && value.AssignedValue.Trim() != "" && value.AssignedValue.ToLower() != "false")
                {

                    //filter from notelist
                    var noteList = _notesList.Where(p => p.DataValue == value.Key.ToString());
                    if (_notesList != null && noteList.Count() > 0)
                    {
                        foreach (var note in noteList)
                        {
                            if (note.DataType == "A")
                            {
                                var notes = GetNotesForA(note.DataValue, model);
                            }

                            var assignedValue = selectedData.Where(x => x.Key == note.DataValue).FirstOrDefault().Value;
                            //if(assignedValue == "FLT")
                            //{
                            //    Console.Write("debug");
                            //}
                            var noteIds = _notesche.Where(p => p.NoteKey == note.DataValue && p.OptKey.ToUpper() == assignedValue.ToUpper())
                                .Select(x => x.NoteId);


                            foreach (var id in noteIds)
                            {
                                string desc = _notes.Where(x => x.NoteId.Equals(id)).FirstOrDefault().NoteText;

                                if (!noteItemsDict.ContainsKey(id))
                                {
                                    noteItemsDict.Add(id, desc);
                                }
                            }
                        }
                    }

                }

            }

        }


        public List<NoteSche> GetNotesForA(string dataValue, string model)
        {
            //
            var owKey = dataValue + "@NoteList";

            // Find Object Key in NoteASOC

            var noteAsoc = _notesAsoc.Where(x => x.OwnerKey == owKey).FirstOrDefault();

            if (noteAsoc != null)
            {
                var owKeyVal = noteAsoc.OwnerKey;
                var objKey = noteAsoc.ObjectKey;

                var assignedVal = selectedData.Where(x => x.Key == objKey).FirstOrDefault().Value;

                if (assignedVal != null && assignedVal != "" && assignedVal != "false")
                {
                    var filteredNoteList = _notesList.Where(x => x.DataKey == dataValue && x.DepKey == assignedVal).FirstOrDefault();

                    if (filteredNoteList != null)
                    {
                        if (filteredNoteList.DataType == "A")
                        {
                            return GetNotesForA(filteredNoteList.DataValue, model);
                        }
                        else
                        {
                            var noteAsocRetry = _notesAsoc.Where(x => x.OwnerKey == filteredNoteList.DataValue).FirstOrDefault();

                            string selectedOp = string.Empty;
                            if (noteAsocRetry.ObjectKey == "PRTU.MODEL")
                            {
                                selectedOp = model;
                            }
                            else
                            {

                                var selectedOpLst = selectedData.Where(x => x.Key == noteAsocRetry.ObjectKey);

                                if (!selectedOpLst.Any())
                                {
                                    return new List<NoteSche>();

                                }
                                selectedOp = selectedOpLst.FirstOrDefault().Value;
                            }

                            var getNotes = _notesche.Where(x => x.NoteKey == noteAsocRetry.OwnerKey && x.DepKey == selectedOp).ToList();

                            return getNotes;
                        }
                    }
                }
            }

            return new List<NoteSche>();
        }
    }
}
