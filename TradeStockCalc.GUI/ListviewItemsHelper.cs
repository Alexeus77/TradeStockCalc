using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TradeStockCalc.GUI
{
    static class ListviewItemsHelper
    {
        public static void AddItems(this ListView.ListViewItemCollection listItems,
            params object[] items)
        {
            var listItem = listItems.Add(new ListViewItem(items.First().ToString()));

            items.Skip(1).Select(item =>
                listItem.SubItems.Add(
                    new ListViewItem.ListViewSubItem(listItem, item.ToString()))).ToList();
        }

        public static void AddColumnsHeaders(this ListView listView,
            params object[] items)
        {
            for(int i = 0; i< items.Count();i++)
            {
                string columnCaption = items[i].ToString();
                listView.Columns.Add(columnCaption,
                    TextRenderer.MeasureText(columnCaption, listView.Font).Width + 30);
            }
        }


    }
}
