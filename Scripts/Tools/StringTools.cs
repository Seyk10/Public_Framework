namespace MECS.Tools
{
    //* Tools used on operations related with strings
    public static class StringTools
    {
        //Method, return and uniq ID based on given name and hash
        public static string GetUniqID(string name, object entity)
        {
            string id = null,
            hasCode = entity.GetHashCode().ToString();

            //Check if name already contains id
            if (name.Contains(hasCode))
                id = name;
            //Set ID
            else
                id = name + "_" + hasCode;

            return id;
        }
    }
}