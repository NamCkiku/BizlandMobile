using System.ComponentModel;

namespace Bizland.Utilities.Enums
{
    public enum ApiEndpointTypes
    {
        [Description("http://api.vietnamcnn.vn")]
        ServerCNN,

        [Description("http://api.bagroup.vn")]
        ServerThat,

        [Description("http://10.1.11.113:8030")]
        ServerNamth,

        [Description("http://192.168.1.68:8012")]
        ServerTest,

        [Description("http://125.212.226.154:5990")]
        ServerTestCNN,

        [Description("http://10.1.11.102:2222")]
        ServerPhuongPV,

        [Description("http://125.212.226.154:3990")]
        ServerVNSAT,

        [Description("http://apiviview.bagroup.vn")]
        ServerVIVIEW,

        [Description("http://125.212.226.154:4990")]
        ServerGISVIET,

        [Description("http://apivms.bagroup.vn")]
        ServerVMS,
    }
}