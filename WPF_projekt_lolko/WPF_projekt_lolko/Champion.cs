using System;

namespace WPF_projekt_lolko
{
    public class Champion
    {
        public int Id { get; set; }
        public string Jmeno { get; set; }
        public string Role { get; set; }
        public string Region { get; set; }
        public int Winrate { get; set; }
        public bool JeOdemceny { get; set; }
    }
}