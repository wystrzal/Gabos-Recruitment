using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Gabos_Recruitment.Module.BusinessObjects.ApiModels
{
    [DomainComponent]
    [DefaultClassOptions]
    [Appearance("NullSubjectsEntityList", AppearanceItemType = "ViewItem", TargetItems = "Subjects",
        Criteria = "Subjects == null || Subjects.Count == 0", Context = "DetailView", Visibility = ViewItemVisibility.Hide, Priority = 1)]
    public class EntityList : BaseNonPersistentObject, IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        public EntityList()
        {
            Oid = Guid.NewGuid();
        }

        private List<Entity> subjects;
        public List<Entity> Subjects
        {
            get { return subjects; }
            set
            {
                subjects = value;
                OnPropertyChanged();
            }
        }

        private string requestDateTime;
        public string RequestDateTime
        {
            get { return requestDateTime; }
            set
            {
                requestDateTime = value;
                OnPropertyChanged();
            }
        }

        private string requestId;
        public string RequestId
        {
            get { return requestId; }
            set
            {
                requestId = value;
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
