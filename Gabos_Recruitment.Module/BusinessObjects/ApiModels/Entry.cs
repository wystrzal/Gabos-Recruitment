using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using Gabos_Recruitment.Module.BusinessObjects.ApiModels.Error;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Gabos_Recruitment.Module.BusinessObjects.ApiModels
{
    [DomainComponent]
    [DefaultClassOptions]
    [Appearance("NullApiException", AppearanceItemType = "ViewItem", TargetItems = "Error",
        Criteria = "Error == null", Context = "DetailView", Visibility = ViewItemVisibility.Hide, Priority = 1)]
    [Appearance("NullSubjectsEntry", AppearanceItemType = "ViewItem", TargetItems = "Subjects",
        Criteria = "Subjects == null || Subjects.Count == 0", Context = "DetailView", Visibility = ViewItemVisibility.Hide, Priority = 1)]
    public class Entry : BaseNonPersistentObject, IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        public Entry()
        {
            Oid = Guid.NewGuid();
        }

        private string identifier;
        public string Identifier
        {
            get { return identifier; }
            set
            {
                identifier = value;
                OnPropertyChanged();
            }
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

        private ApiException error;
        public ApiException Error
        {
            get { return error; }
            set
            {
                error = value;
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
