namespace HH.Identity.Settings
{
    public class JWT
    {
        public string Key { get; set; }//Секретный ключ используещийся для шиформания 
        
        public string Issuer { get; set; }//определяет принцип выдавший JWT
        
        public string Audience { get; set; }//определяет получателей, для которых предназначен JWT
        
        public double DurationInMinutes { get; set; }//определяет количество минут, в течение которых сгенерированный JWT будет оставаться действительным
    }
}