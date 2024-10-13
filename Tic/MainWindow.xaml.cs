using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isDiscordTurn = true;
        private ImageSource discordImage;
        private ImageSource rknImage;
        private int movesCount = 0;
        private Button[,] buttons = new Button[3, 3];
        private int?[,] gameState = new int?[3, 3];

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                InitializeGameGrid();
                discordImage = new BitmapImage(new Uri("pack://application:,,,/Pictures/ds.jpg"));
                rknImage = new BitmapImage(new Uri("pack://application:,,,/Pictures/rkn.jpg"));
            }
            catch (Exception ex) { MessageBox.Show($"Ошибка при инициализации: {ex.Message}", "Ошибка"); }
        }

        private void InitializeGameGrid()
        {
            buttons[0, 0] = Button1;
            buttons[0, 1] = Button2;
            buttons[0, 2] = Button3;
            buttons[1, 0] = Button4;
            buttons[1, 1] = Button5;
            buttons[1, 2] = Button6;
            buttons[2, 0] = Button7;
            buttons[2, 1] = Button8;
            buttons[2, 2] = Button9;

            // Инициализируем массив состояния игры (все ячейки пустые)
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    gameState[i, j] = null;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            // Ищем индексы нажатой кнопки
            int row = -1, col = -1;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (buttons[i, j] == button)
                    {
                        row = i;
                        col = j;
                        break;
                    }
                }
            }

            if (gameState[row, col] != null)
                return;

            Image image = new Image();

            // Устанавливаем изображение в зависимости от текущего хода
            if (isDiscordTurn)
            {
                gameState[row, col] = 0; // Discord
                image.Source = discordImage;
            }
            else
            {
                gameState[row, col] = 1; // РКН
                image.Source = rknImage;
            }

            button.Content = image;
            movesCount++;

            if (CheckForWinner())
            {
                MessageBox.Show($"{(isDiscordTurn ? "Discord" : "РКН")} победил!", "Победа");
                RestartGame();
                return;
            }

            if (movesCount == 9)
            {
                MessageBox.Show("Ничья!", "Конец игры");
                RestartGame();
                return;
            }

            isDiscordTurn = !isDiscordTurn;
        }
    }
}