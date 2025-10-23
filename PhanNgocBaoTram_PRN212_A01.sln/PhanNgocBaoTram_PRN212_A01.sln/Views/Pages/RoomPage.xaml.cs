using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FUMiniHotelSystem.Business.Services;
using FUMiniHotelSystem.Data.Models;
using FUMiniHotelSystem.Data.Database;

namespace PhanNgocBaoTramWPF.Views.Pages
{
    public partial class RoomPage : Page
    {
        private readonly RoomService _roomService = new();

        public RoomPage()
        {
            InitializeComponent();
            LoadRoomTypes();
            LoadData();
        }

        private void LoadRoomTypes()
        {
            cmbRoomType.ItemsSource = FUMiniHotelContext.Instance.RoomTypes;
            cmbRoomType.DisplayMemberPath = "RoomTypeName";
            cmbRoomType.SelectedValuePath = "RoomTypeID";
        }

        private void LoadData()
        {
            dgRooms.ItemsSource = _roomService.GetAll().ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNumber.Text) ||
                    string.IsNullOrWhiteSpace(txtPrice.Text) ||
                    !decimal.TryParse(txtPrice.Text, out var price) ||
                    !int.TryParse(txtCapacity.Text, out var capacity) ||
                    cmbRoomType.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin phòng hợp lệ.");
                    return;
                }

                var room = new Room
                {
                    RoomNumber = txtNumber.Text?.Trim() ?? string.Empty,
                    RoomDescription = txtDescription.Text?.Trim() ?? string.Empty,
                    RoomPricePerDate = price,
                    RoomMaxCapacity = capacity,
                    RoomTypeID = (int)cmbRoomType.SelectedValue,
                    RoomStatus = 1
                };

                _roomService.Create(room);
                LoadData();
                ClearForm();
                MessageBox.Show("Thêm phòng thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm phòng: {ex.Message}");
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgRooms.SelectedItem is Room selectedRoom)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(txtNumber.Text) ||
                        string.IsNullOrWhiteSpace(txtPrice.Text) ||
                        !decimal.TryParse(txtPrice.Text, out var price) ||
                        !int.TryParse(txtCapacity.Text, out var capacity) ||
                        cmbRoomType.SelectedValue == null)
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ thông tin phòng hợp lệ.");
                        return;
                    }

                    selectedRoom.RoomNumber = txtNumber.Text?.Trim() ?? string.Empty;
                    selectedRoom.RoomDescription = txtDescription.Text?.Trim() ?? string.Empty;
                    selectedRoom.RoomPricePerDate = price;
                    selectedRoom.RoomMaxCapacity = capacity;
                    selectedRoom.RoomTypeID = (int)cmbRoomType.SelectedValue;

                    _roomService.Update(selectedRoom);
                    LoadData();
                    ClearForm();
                    MessageBox.Show("Cập nhật phòng thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật phòng: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phòng cần sửa.");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgRooms.SelectedItem is Room selectedRoom)
            {
                if (MessageBox.Show($"Bạn có chắc muốn xóa phòng {selectedRoom.RoomNumber}?",
                    "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        _roomService.Delete(selectedRoom.RoomID);
                        LoadData();
                        ClearForm();
                        MessageBox.Show("Xóa phòng thành công!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa phòng: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phòng cần xóa.");
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearch.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(keyword))
            {
                LoadData();
                return;
            }

            var result = _roomService.GetAll()
                .Where(r => r.RoomNumber.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                           r.RoomDescription.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();

            dgRooms.ItemsSource = result;
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Clear();
            LoadData();
        }

        private void ClearForm()
        {
            txtNumber.Clear();
            txtDescription.Clear();
            txtPrice.Clear();
            txtCapacity.Clear();
            cmbRoomType.SelectedIndex = -1;
        }

        private void DgRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgRooms.SelectedItem is Room selectedRoom)
            {
                txtNumber.Text = selectedRoom.RoomNumber;
                txtDescription.Text = selectedRoom.RoomDescription;
                txtPrice.Text = selectedRoom.RoomPricePerDate.ToString();
                txtCapacity.Text = selectedRoom.RoomMaxCapacity.ToString();
                cmbRoomType.SelectedValue = selectedRoom.RoomTypeID;
            }
        }
    }
}
