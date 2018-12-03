/*
 * Diese Klasse ermöglicht in Datagridview und co den Zugriff auf das Contextmenu-Item
 * Quelle:
 * https://www.mycsharp.de/wbb2/thread.php?postid=3615148
 */
using System;
using System.Drawing;
using System.ComponentModel;

namespace System.Windows.Forms {
   [System.ComponentModel.DesignerCategory("Code")]
   public class ContextMenuEx : ContextMenuStrip {

      /// <summary>
      /// mousePosition over the control, on which the ContextMenuEx at last was opened
      /// </summary>
      [ToolboxItem(false)]
      public Point OpenClickLocation { get; private set; }

      protected override void OnOpened(EventArgs e) {
         OpenClickLocation = SourceControl.PointToClient(Control.MousePosition);
         base.OnOpened(e);
      }
   }
}
