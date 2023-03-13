using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows;
using HandyControl.Themes;
using Syncfusion.Windows.Controls;
using Syncfusion.Windows.Shared;
using Syncfusion.SfSkinManager;
using Syncfusion.Windows;
namespace Inventarisation
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        //Register Syncfusion license
        public App() {

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt/QHRqVVhjVFpFdEBBXHxAd1p/VWJYdVt5flBPcDwsT3RfQF5jSH1TdkFgWHpccnxdTg==;Mgo+DSMBPh8sVXJ0S0J+XE9HflRDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS31Td0ZjWXtdcXFXRWBYWQ==;ORg4AjUWIQA/Gnt2VVhkQlFadVdJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxQdkRgWX9ac3VQRGNeUUc=;MTAyODM0OEAzMjMwMmUzNDJlMzBHZy9wU082dEZUMjFhaFhWOS9GaWt3YkdGQTMzNEJjajZRY0dvZ2h5L21RPQ==;MTAyODM0OUAzMjMwMmUzNDJlMzBIM3Q0WkcxRGswTiszbDJUdDdvNG9HbWlmdmJ1amtlSmVLV1hoWC9ZWmI0PQ==;NRAiBiAaIQQuGjN/V0Z+WE9EaFxKVmJLYVB3WmpQdldgdVRMZVVbQX9PIiBoS35RdUViWn9edHdUQ2JfWEJ+;MTAyODM1MUAzMjMwMmUzNDJlMzBMS3NEaGhDa0RFZkVZVHJ2akJlR3dHb1BxZkwvQjg5OHdYVUhoV3dDdXg0PQ==;MTAyODM1MkAzMjMwMmUzNDJlMzBrYlA4amNoeEFWOVZ5NkFCNFhPb3JPQlNFWWk1UUJRdTROMHNkdmdUcC9RPQ==;Mgo+DSMBMAY9C3t2VVhkQlFadVdJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxQdkRgWX9ac3VQRGRbWUI=;MTAyODM1NEAzMjMwMmUzNDJlMzBHVVNXRFFEN1hqb2FKTFphR2lnMlkvZmZiRlczOUNWb3BablNzMWh6Y1VjPQ==;MTAyODM1NUAzMjMwMmUzNDJlMzBJamk5bUtBck5qV3RsRFZUMkNKSzIremFUSWtKUUZiZEVRRVZkNllNZlY0PQ==;MTAyODM1NkAzMjMwMmUzNDJlMzBMS3NEaGhDa0RFZkVZVHJ2akJlR3dHb1BxZkwvQjg5OHdYVUhoV3dDdXg0PQ==");
            //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt/QHRqVVhjVFpGaV1AQmFJfFBmQ2lZfFR0cUU3HVdTRHRcQlxhSn5VdkZnX3pXcXM=;Mgo+DSMBPh8sVXJ0S0J+XE9HflRAQmFLYVF2R2BJeVRwd19CZUwgOX1dQl9gSX9RdUVlWXxadnJUTmI=;ORg4AjUWIQA/Gnt2VVhkQlFadVdJX3xLfEx0RWFab1h6dFZMZFlBNQtUQF1hSn5SdEZjX39ddHJTQGRe;OTg3OTQ4QDMyMzAyZTM0MmUzMGxHeEVXczZ1WjFXVmlTbWY1Q2tWMlVwRWQ3MkhUNWgwdlNOQmdkNUdPZTQ9;OTg3OTQ5QDMyMzAyZTM0MmUzMFZpU0dsSWxOYnhGbUpOYm1MRzZSd1lJZDhiakhFU29ZOU9HTXplRFQ5NWc9;NRAiBiAaIQQuGjN/V0Z+WE9EaFxKVmFWfFFpR2NbfE52flVFalhUVAciSV9jS31Td0dgWXlec3BTQWdVUw==;OTg3OTUxQDMyMzAyZTM0MmUzMEs5S0xQLytVaUdiYjFiWkFNVC9HNXMwQlEvU2hObUhhMGh1R096YitMbms9;OTg3OTUyQDMyMzAyZTM0MmUzMGdpM1FobmpQY0hySlg4SGtRL2gzam1NSU96T0VFVjJVRjc1c1Nxbzkxc289;Mgo+DSMBMAY9C3t2VVhkQlFadVdJX3xLfEx0RWFab1h6dFZMZFlBNQtUQF1hSn5SdEZjX39ddHJdQmle;OTg3OTU0QDMyMzAyZTM0MmUzMFdDWGpCT29jcVU2NGxlT2RXTkJTOCtPWFAzWThqaHhmbzlSYlNVdzF2dUE9;OTg3OTU1QDMyMzAyZTM0MmUzMFBMZkJRU1JhdG5yai9lb3FWcjRtc25lZjY2S3R2SzBOWk1ENkFRVmFDb0E9;OTg3OTU2QDMyMzAyZTM0MmUzMEs5S0xQLytVaUdiYjFiWkFNVC9HNXMwQlEvU2hObUhhMGh1R096YitMbms9");
            
    }
        public static VisualStyles CurrentTheme { get; set; }
    }


    

}
