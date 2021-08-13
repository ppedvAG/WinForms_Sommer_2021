using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CustomControls
{
    class MyButton : Button
    {
        protected override void OnPaint(PaintEventArgs pevent)
        {
            //base.OnPaint(pevent);
            pevent.Graphics.FillRectangle(new SolidBrush(Parent.BackColor), this.ClientRectangle);
            pevent.Graphics.FillEllipse(new LinearGradientBrush(ClientRectangle, Color.Pink, BackColor, 90f), this.ClientRectangle);

            if (ClientRectangle.Contains(PointToClient(Cursor.Position)))
            {
                pevent.Graphics.DrawEllipse(new Pen(Color.Black,12f), this.ClientRectangle);
            }

            var sf = new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };
            pevent.Graphics.DrawString(Text, SystemFonts.DefaultFont, Brushes.Red, ClientRectangle, sf);
        }
    }
}
