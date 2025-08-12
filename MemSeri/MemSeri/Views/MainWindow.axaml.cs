using System;
using System.Linq;
using System.Text;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MemoryPack;
namespace MemSeri.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ConvertButton.Click += OnConvertButtonClicked;
            Serialize.Click += DisableDeserialize;
            Deserialize.Click += DisableSerialize;
        }
        private void DisableDeserialize(object? sender, RoutedEventArgs e)
        {
            SerializedInput.IsVisible = false;
            JsonModelTextBox.IsVisible = true;

        }
        private void DisableSerialize(object? sender, RoutedEventArgs e)
        {
            JsonModelTextBox.IsVisible = false;
            SerializedInput.IsVisible = true;
        }
        private void OnConvertButtonClicked(object? sender, RoutedEventArgs e)
        {
            try
            {
                if (Serialize.IsChecked ?? false)
                {
                    var data = SerializeData(JsonModelTextBox.Text);
                    OutputTextBlock.Text = FormatSerializedData(data);
                }
                if (Deserialize.IsChecked ?? false)
                {
                    byte[] inputBytes = ConvertHexStringToByteArray(SerializedInput.Text);
                    var data = DserializeData(inputBytes);
                    OutputTextBlock.Text = data;
                }
            }
            catch (Exception ex)
            {
                OutputTextBlock.Text = $"Error: {ex.Message}";
            }
        }

        private byte[] ConvertHexStringToByteArray(string hexString)
        {
            hexString = hexString.Replace(" ", "").Replace("-", "");

            return Enumerable.Range(0, hexString.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hexString.Substring(x, 2), 16))
                .ToArray();
        }

        private string FormatSerializedData(byte[] data)
        {
            string hexRepresentation = BitConverter.ToString(data);

            string base64Representation = Convert.ToBase64String(data);

            return $"Hex: {hexRepresentation}\n\n" +
                   $"Base64: {base64Representation}\n\n" +
                   $"Byte Length: {data.Length}\n\n" +
                   $"Raw Hex (no separators): {string.Concat(data.Select(b => b.ToString("X2")))}";
        }

        private byte[] SerializeData(string input)
        {
            try
            {
                if (string.IsNullOrEmpty(input))
                    return Array.Empty<byte>();

                var options = MemoryPackSerializerOptions.Default;
                var data = MemoryPackSerializer.Serialize(input, options);

                return data ?? Array.Empty<byte>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Serialization Error: {ex}");
                return Array.Empty<byte>();
            }
        }
        private string DserializeData(byte[] input)
        {
            try
            {
                if (input == null || input.Length == 0)
                    return string.Empty;

                var data = MemoryPackSerializer.Deserialize<string>(input);
                return data ?? string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Deserialization Error: {ex}");
                return string.Empty;
            }
        }

    }
}
