using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace WPF_projekt_lolko
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Champion> Championi { get; set; }
        public ObservableCollection<string> Regiony { get; set; }

        private Champion aktualniChampion = null;

        public MainWindow()
        {
            InitializeComponent();

            Championi = new ObservableCollection<Champion>();

            Regiony = new ObservableCollection<string>
            {
                "Demacia",
                "Noxus",
                "Ionia",
                "Freljord",
                "Piltover",
                "Zaun",
                "Shurima",
                "Targon",
                "Bilgewater",
                "Shadow Isles",
                "Ixtal",
                "Bandle City",
                "Void"
            };

            dgChampioni.ItemsSource = Championi;
            cbRegion.ItemsSource = Regiony;
        }

        private void Pridat_Click(object sender, RoutedEventArgs e)
        {
            if (!Validace(out int winrate)) return;

            Champion novy = new Champion
            {
                Id = Championi.Any() ? Championi.Max(c => c.Id) + 1 : 1,
                Jmeno = txtJmeno.Text,
                Role = txtRole.Text,
                Region = cbRegion.SelectedItem as string,
                Winrate = winrate,
                JeOdemceny = chkOdemceny.IsChecked == true
            };

            Championi.Add(novy);
            Vycistit();
        }

        private void Nacist_Click(object sender, RoutedEventArgs e)
        {
            if (dgChampioni.SelectedItem is Champion vybrany)
            {
                aktualniChampion = vybrany;

                txtJmeno.Text = vybrany.Jmeno;
                txtRole.Text = vybrany.Role;
                txtWinrate.Text = vybrany.Winrate.ToString();
                chkOdemceny.IsChecked = vybrany.JeOdemceny;
                cbRegion.SelectedItem = vybrany.Region;
            }
        }

        private void Upravit_Click(object sender, RoutedEventArgs e)
        {
            if (aktualniChampion == null)
            {
                MessageBox.Show("Nejdřív načti championa!");
                return;
            }

            if (!Validace(out int winrate)) return;

            aktualniChampion.Jmeno = txtJmeno.Text;
            aktualniChampion.Role = txtRole.Text;
            aktualniChampion.Region = cbRegion.SelectedItem as string;
            aktualniChampion.Winrate = winrate;
            aktualniChampion.JeOdemceny = chkOdemceny.IsChecked == true;

            dgChampioni.Items.Refresh();
            Vycistit();
        }

        private void Smazat_Click(object sender, RoutedEventArgs e)
        {
            if (dgChampioni.SelectedItem is Champion vybrany)
            {
                var result = MessageBox.Show("Opravdu smazat?", "Potvrzení", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    Championi.Remove(vybrany);
                }
            }
        }

        private bool Validace(out int winrate)
        {
            winrate = 0;

            if (string.IsNullOrWhiteSpace(txtJmeno.Text))
            {
                MessageBox.Show("Zadej jméno!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtRole.Text))
            {
                MessageBox.Show("Zadej roli!");
                return false;
            }

            if (!int.TryParse(txtWinrate.Text, out winrate))
            {
                MessageBox.Show("Winrate musí být číslo!");
                return false;
            }

            if (winrate < 0)
            {
                MessageBox.Show("Winrate nesmí být záporný!");
                return false;
            }

            if (cbRegion.SelectedItem == null)
            {
                MessageBox.Show("Vyber region!");
                return false;
            }

            return true;
        }

        private void Vycistit()
        {
            txtJmeno.Text = "";
            txtRole.Text = "";
            txtWinrate.Text = "";
            cbRegion.SelectedItem = null;
            chkOdemceny.IsChecked = false;
            aktualniChampion = null;
        }
    }
}