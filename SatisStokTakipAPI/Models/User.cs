namespace SatisStokTakipAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; } // şifreler asla metin halinde saklanmamalı, ancak bu örnekte basitlik için böyle yapıyoruz
    }



}
