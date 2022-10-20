namespace SCGP.COA.BUSINESSLOGIC.Models
{
    public class MenuModel
    {
    }

    public class MenuTree
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
        public int Level { get; set; }
        public string Icon { get; set; }
        public bool? ShowItems { get; set; }
        public List<MenuTree> Items { get; set; }
    }
}
