using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bizland.Core.Events
{
    public class SelectCountryEvent : PubSubEvent<CountryCode>
    {
    }
}
