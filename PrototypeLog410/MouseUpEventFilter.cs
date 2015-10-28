using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrototypeLog410
{
    class MouseUpEventFilter : IMessageFilter
    {
        public delegate void mouseUpCallback(int mouseX, int mouseY);

        private mouseUpCallback callback;

        public MouseUpEventFilter(mouseUpCallback callback)
        {
            this.callback = callback;
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 0x0202)
            {
                callback(Cursor.Position.X, Cursor.Position.Y);
            }

            return false;
        }
    }
}
