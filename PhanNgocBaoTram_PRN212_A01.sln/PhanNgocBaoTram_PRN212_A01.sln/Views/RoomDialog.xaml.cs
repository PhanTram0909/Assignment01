using System;
using System.Windows;
using FUMiniHotelSystem.Business.Services;
using FUMiniHotelSystem.Data.Models;

namespace PhanNgocBaoTramWPF.Views
{
    public partial class RoomDialog : Window
    {
        private readonly RoomService _service = new RoomService();
        private readonly int? _roomId;

        public RoomDialog(int? roomId = null)
        {
            InitializeComponent();
            _roomId = roomId;

            if (_roomId.HasValue)
                LoadRoom(_roomId.Value);
        }

        private void LoadRoom(int id)
        {
            var room = _service.GetById(id);
            if (room != null)
            {
                txtNumber.Text = room.RoomNumber;
                txtTypeID.Text = room.RoomTypeID.ToString();
                txtCapacity.Text = room.RoomMaxCapacity.ToString();
                txtPrice.Text = room.RoomPricePerDate.ToString();
            }
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNumber.Text))
                    throw new Exception("Room number is required.");

                var room = new Room
                {
                    RoomNumber = txtNumber.Text.Trim(),
                    RoomTypeID = int.Parse(txtTypeID.Text),
                    RoomMaxCapacity = int.Parse(txtCapacity.Text),
                    RoomPricePerDate = decimal.Parse(txtPrice.Text)
                };

                if (_roomId.HasValue)
                {
                    room.RoomID = _roomId.Value;
                    _service.Update(room); // dùng Update của RoomService
                }
                else
                {
                    _service.Create(room); // dùng Create thay vì Add
                }

                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
