using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesProcess
{
    public partial class Note
    {
        public string Model { get; set; }

        public int NoteId { get; set; }

        public string UOM { get; set; }

        public int Weighting { get; set; }
        public string NoteText { get; set; }
    }

    public partial class NoteASOC
    {

        public string Model { get; set; }
        public string OwnerKey { get; set; }
        public int? ObjectSeq { get; set; }
        public string ObjectType { get; set; }
        public string ObjectKey { get; set; }
    }

    public partial class NoteList
    {
        public string Model { get; set; }

        public string DepKey { get; set; }

        public int DataSeq { get; set; }
        public string DataType { get; set; }
        public string DataValue { get; set; }

        public string DataKey { get; set; }

    }

    public partial class NoteSche
    {

        public string Model { get; set; }
        
        public string OptKey { get; set; }

        public string NoteKey { get; set; }
        public string DepKey { get; set; }

        public int NoteId { get; set; }

        public int Sequence { get; set; }


    }

    public partial class DisplayVariables
    {
        public string Key { get; set; }

        public string AssignedValue { get; set; }
    }

    public partial class SelectedData
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
