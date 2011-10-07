
namespace DareToEscape.Editor
{
    enum ItemType {Entity, Tile}

    class EditorItem
    {   
        public ItemType Type { get; set; }
        public string Code { get; set; }
        public string CodeAbove { get; set; }
        public string CodeBelow { get; set; }
        public string CodeLeft { get; set; }
        public string CodeRight { get; set; }
        public bool Unique { get; set; }
        public bool? Passable { get; set; }
        public int? TileID { get; set; }
    }
}
