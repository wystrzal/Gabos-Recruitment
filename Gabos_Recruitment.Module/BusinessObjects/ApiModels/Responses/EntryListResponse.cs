using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Gabos_Recruitment.Module.BusinessObjects.ApiModels.Responses
{
    [DomainComponent]
    [DefaultClassOptions]
    public class EntryListResponse : BaseNonPersistentObject, IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        public EntryListResponse()
        {
            Oid = Guid.NewGuid();
        }

        private EntryList result;
        [ExpandObjectMembers(ExpandObjectMembers.Always)]
        public EntryList Result
        {
            get { return result; }
            set
            {
                result = value;
                OnPropertyChanged();
            }
        }

        #region IObjectSpaceLink members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIObjectSpaceLinktopic.aspx)
        // Use the Object Space to access other entities from IXafEntityObject methods (see https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113707.aspx).
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { objectSpace = value; }
        }
        #endregion
    }
}
