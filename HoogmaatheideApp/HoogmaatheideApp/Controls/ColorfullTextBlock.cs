using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Media;

namespace HoogmaatheideApp.Controls
{
    public class ColorfullTextBlockMaker
    {
       
        public static void MakeTextblockColorfull(TextBlock textblock, string text, List<string> keywords, FontWeight fontWeight )
        {
            var strings = text.Split(' ');
            var foregroundBrush =  new SolidColorBrush((Color)Application.Current.Resources["PhoneAccentColor"]);


            textblock.Inlines.Clear();
            textblock.Inlines.Add(new Run { Text = @"""" ,FontWeight = fontWeight, Foreground = foregroundBrush});
            strings.ToList().ForEach(s =>
                                         {
                                             var run = new Run{Text = s + " "};
                                             foreach (var keyword in keywords)
                                             {
                                                 if (s.ToLower().Contains(keyword.ToLower()))
                                                 {
                                                     run.Foreground = foregroundBrush;
                                                     run.FontWeight = fontWeight;
                                                     break;
                                                 }
                                             }

                                             textblock.Inlines.Add(run);
                                         });
            textblock.Inlines.Add(new Run { Text = @"""", FontWeight = fontWeight, Foreground = foregroundBrush });
           
        }
    }
}
