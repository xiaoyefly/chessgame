using System.Collections.Generic;
using Assets.Scripts.Common.Utils;
using Assets.Scripts.Common.Windows.TabPages;
using UnityEngine;

namespace Assets.Scripts.Hall.View.SysNoticeWindows
{
    /// <summary>
    /// 公告
    /// </summary>
    public class AnnouncementWindow : YxTabPageWindow
    {
        public AnnouncementItemView ItemView;
        [Tooltip("无数据显示")]
        public GameObject NoDataSign;
        protected override void OnAwake()
        {
            base.OnAwake();
            CheckIsStart = true;
        }

        protected override void ActionCallBack()
        {
            var list = GetData<List<object>>();
            if (list == null) return;
            var count = list.Count;
            NoDataSign.TrySetComponentValue(count == 0);
            TabDatas = new TabData[count];
            for (var i = 0; i < count; i++)
            {
                var obj = list[i];
                if (!(obj is Dictionary<string, object>)) continue;
                var dict = obj as Dictionary<string, object>;
                var adata = new AnnouncementData(dict); 
                var tdata = new TabData
                    {
                        Name = adata.Title,
                        Data = adata
                    };
                TabDatas[i] = tdata;
            }
            if (TabDatas.Length < 1) return;
            var td = TabDatas[0];
            td.StarttingState = true;
            UpdateView(-1);
        }

        public override void OnTableClick(YxTabItem tableView)
        {
            if (!tableView.GetToggle().value) return;
            var tdata = tableView.GetData<TabData>();
            if (tdata == null) return;
            ItemView.UpdateView(tdata.Data);
        }

        protected override void TabSelectAction(YxTabItem tableView)
        {
            var tdata = tableView.GetData<TabData>();
            if (tdata == null) return;
            ItemView.UpdateView(tdata.Data);
        }
    }
}
