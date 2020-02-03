using Bizland.Utilities.Enums;

namespace Bizland.Core.Resource
{
    public partial class MobileResource
    {
        public static string Common_Lable_More => Get(MobileResourceNames.Common_Lable_More, "Xem thêm..", "More...");

        public static string Common_Lable_NotFound => Get(MobileResourceNames.Common_Lable_NotFound, "Không tìm thấy dữ liệu..", "Not found...");

        public static string Common_Message_Processing => Get(MobileResourceNames.Common_Loading_Processing1, "Đang xử lý...", "Processing...");

        public static string Common_Message_Loading => Get(MobileResourceNames.Common_Message_Loading, "Đang tải dữ liệu...", "Loading...");

        public static string Common_Message_Warning => Get(MobileResourceNames.Common_Message_Warning, "Cảnh báo", "Warning");

        public static string Common_Label_BAGPS => Get(MobileResourceNames.Common_Label_BAGPS, "Mobile GPS", "Mobile GPS");
        public static string Common_Button_OK => Get(MobileResourceNames.Common_Button_OK, "Đồng ý", "OK");

        public static string Common_Button_Send => Get(MobileResourceNames.Common_Button_Send, "Gửi", "Send");
        public static string Common_Button_View => Get(MobileResourceNames.Common_Button_View, "Xem", "View");
        public static string Common_Button_Save => Get(MobileResourceNames.Common_Button_Save, "Lưu", "Save");
        public static string Common_Button_Cancel => Get(MobileResourceNames.Common_Button_Cancel, "Huỷ", "Cancel");
        public static string Common_Button_Yes => Get(MobileResourceNames.Common_Button_Yes, "Có", "Yes");
        public static string Common_Button_No => Get(MobileResourceNames.Common_Button_No, "Không", "No");
        public static string Common_Button_Close => Get(MobileResourceNames.Common_Button_Close, "Đóng", "Close");
        public static string Common_Message_ErrorTryAgain => Get(MobileResourceNames.Common_Message_ErrorTryAgain, "Có lỗi xảy ra, vui lòng thử lại", "Error(s), please try again");

        public static string Common_Message_SearchText => Get(MobileResourceNames.Common_Message_SearchText, "Tìm kiếm", "Search");

        public static string Common_Message_SearchTextValid => Get(MobileResourceNames.Common_Message_SearchTextValid, "Ô tìm kiếm không được chứa các ký tự đặc biệt", "Search text do not contain special characters");

        public static string Common_Label_CameraTitle => Get(MobileResourceNames.Common_Label_CameraTitle, "Camera", "Camera");
        public static string Common_Message_CameraUnavailable => Get(MobileResourceNames.Common_Message_CameraUnavailable, "Camera không khả dụng", "No camera available");
        public static string Common_Message_CameraUnsupported => Get(MobileResourceNames.Common_Message_CameraUnsupported, "Camera không được hỗ trợ", "Camera not supported");
        public static string Common_Message_PickPhotoUnsupported => Get(MobileResourceNames.Common_Message_PickPhotoUnsupported, "Chọn hình ảnh không được hỗ trợ", "Pick photos not supported");
        public static string Common_Message_PickPhotoPermissionNotGranted => Get(MobileResourceNames.Common_Message_PickPhotoPermissionNotGranted, "Có thể quyền truy cập hình ảnh chưa được chấp nhận", "Maybe permission is not granted to photos");
        public static string Common_Message_TakeNewPhoto => Get(MobileResourceNames.Common_Message_TakeNewPhoto, "Chụp ảnh mới", "Take new photo");
        public static string Common_Message_ChooseAvailablePhotos => Get(MobileResourceNames.Common_Message_ChooseAvailablePhotos, "Chọn ảnh có sẵn", "Choose available photos");

        public static string Common_Button_Update => Get(MobileResourceNames.Common_Button_Update, "Cập nhật", "Update");

        public static string Common_Message_Skip => Get(MobileResourceNames.Common_Message_Skip, "Bỏ qua", "Skip");

        #region Validate

        public static string Common_Property_Invalid(string property) => Get(MobileResourceNames.Common_Property_Invalid, string.Format("{0} không hợp lệ", property), string.Format("{0} is not valid", property));

        public static string Common_Property_NullOrEmpty(string property) => Get(MobileResourceNames.Common_Property_NullOrEmpty, string.Format("{0} không được trống", property), string.Format("{0} cannot be empty", property));

        public static string Common_Property_MinLength(string property) => Get(MobileResourceNames.Common_Property_MinLength, string.Format("{0} phải có ít nhất 3 kí tự", property), string.Format("{0} must have at least 3 characters", property));

        public static string Common_Property_DangerousChars(string property) => Get(MobileResourceNames.Common_Property_DangerousChars, string.Format("{0} không được chứa các kí tự ',\",<,>,/,&", property), string.Format("{0} cannot contain characters ',\",<,>,/,&", property));

        public static string Common_Property_NotContainChars(string property, string chars) => Get(MobileResourceNames.Common_Property_NotContainChars, string.Format("{0} không được chứa các kí tự {1}", property, chars), string.Format("{0} cannot contain characters {1}", property, chars));

        public static string Common_Message_RequiredNullOrEmpty => Get(MobileResourceNames.Common_Button_Update, "Trường không được để trống", "This field is required");

        public static string Common_Property_DangerousCharShow(string property, string chardangerous) => Get(MobileResourceNames.Common_Property_DangerousChars, string.Format("{0} không được chứa các kí tự {1}", property.Replace("(*)", "").Trim(), chardangerous), string.Format("{0} cannot contain characters {1}", property.Replace("(*)", "").Trim(), chardangerous));

        #endregion Validate

        #region datetime picker

        public static string Common_Label_TimePicker => Get(MobileResourceNames.Common_Label_TimePicker, "Chọn Giờ", "Time Picker");
        public static string Common_Label_DatePicker => Get(MobileResourceNames.Common_Label_DatePicker, "Chọn Ngày", "Date Picker");
        public static string Common_Label_DateTimePicker => Get(MobileResourceNames.Common_Label_CameraTitle, "Chọn Ngày Giờ", "Date Time Picker");
        public static string Common_Label_Year => Get(MobileResourceNames.Common_Label_Year, "Năm", "Year");
        public static string Common_Label_Month => Get(MobileResourceNames.Common_Label_Month, "Tháng", "Month");
        public static string Common_Label_Day => Get(MobileResourceNames.Common_Label_Day, "Ngày", "Day");
        public static string Common_Label_Day2 => Get(MobileResourceNames.Common_Label_Day2, "N", "D");
        public static string Common_Label_Hour => Get(MobileResourceNames.Common_Label_Hour, "Giờ", "Hour");
        public static string Common_Label_Hour2 => Get(MobileResourceNames.Common_Label_Hour2, "G", "H");
        public static string Common_Label_Minute => Get(MobileResourceNames.Common_Label_Minute, "Phút", "Minute");
        public static string Common_Label_Minute2 => Get(MobileResourceNames.Common_Label_Minute2, "P", "M");
        public static string Common_Label_Second => Get(MobileResourceNames.Common_Label_Second, "Giây", "Second");
        public static string Common_Label_Duration => Get(MobileResourceNames.Common_Label_Duration, "Thời gian", "Duration");
        public static string Common_Label_Duration2 => Get(MobileResourceNames.Common_Label_Duration2, "T.Gian", "Dur");

        #endregion datetime picker
    }
}