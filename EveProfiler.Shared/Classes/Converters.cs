using EveProfiler.Logic.CharacterAttributes;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace EveProfiler.Shared.Classes
{
    public class CharacterImageUriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language) => 
            $"https://image.eveonline.com/Character/{value}_256.jpg";

        // No need to implement converting back on a one-way binding 
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class CorporationImageUriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language) =>
            $"http://image.eveonline.com/Corporation/{value}_256.png";

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class RaceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            if (!string.IsNullOrEmpty((string)value))
            {
                return new BitmapImage(new Uri($"ms-appx:///Assets/Images/{((string)value).ToLower()}_race.png"));
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BalanceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            if (value != null)
            {
                return $"Balance: {((double)value).ToString("##,#.##")} ISK";
            }
                
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SecStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            if (value != null)
            {
                return $"Security Status: {((double)value).ToString("0.##", CultureInfo.InvariantCulture)}";
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BirthdayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            if (value != null)
                return "Birthday: " + ((DateTime)value).ToString("G");

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            DateTime mailDate = (DateTime)value;

            if (DateTime.UtcNow == mailDate.Date)
            {
                return mailDate.ToString("T");
            }
            else
            {
                return mailDate.ToString("m");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ExtensionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            Mail item = value as Mail;
            if (item == null || !item.IsExtended)
            {
                return new GridLength(0);
            }
            else
            {
                return GridLength.Auto;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SkillTimeLeftConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            string timeLeft = string.Empty;

            if (!((TimeSpan)value).Equals(TimeSpan.Zero))
            {
                TimeSpan timeSpan = (TimeSpan)value;

                if (timeSpan.Days > 1)
                    timeLeft += timeSpan.ToString(@"%d' Days '");
                if (timeSpan.Days == 1)
                    timeLeft += timeSpan.ToString(@"%d' Day '");
                if (timeSpan.Hours > 1)
                    timeLeft += timeSpan.ToString(@"%h' Hours '");
                if (timeSpan.Hours == 1)
                    timeLeft += timeSpan.ToString(@"%h' Hour '");
                if (timeSpan.Minutes > 1)
                    timeLeft += timeSpan.ToString(@"%m' Minutes '");
                if (timeSpan.Minutes == 1)
                    timeLeft += timeSpan.ToString(@"%m' Minute '");

                if (timeSpan.Seconds == 1)
                    timeLeft += timeSpan.ToString(@"%s' Second'");
                else
                    timeLeft += timeSpan.ToString(@"%s' Seconds'");
            }

            return timeLeft;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            HtmlDocument doc;
            //ObservableCollection<cTitleId> ocTitles = value as ObservableCollection<cTitleId>;

            //if (ocTitles.Count > 0)
            //{
            //    doc = new HtmlDocument();
            //    doc.LoadHtml(ocTitles[0].titleName);
            //    return doc.DocumentNode.InnerText;
            //}
            //else
            //{
            return string.Empty;
            //}
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DateTimeToDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture) => 
            ((DateTime)value).ToString("d");

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MailBodyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            if (value != null)
            {
                string html = ((string)value).Replace("<br>", "\r\n");

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);
                return doc.DocumentNode.InnerText;
            }
            else
            {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }

    public class AttributeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture) => 
            $"{value} points";

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SkillCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            //List<cEVESkill> injectedSkills = new HashSet<cEVESkill>(value as ObservableCollection<cEVESkill>)
            //    .Where(x => x.characterSkill != null).ToList();

            //return string.Format("{0} of {1} Skills", injectedSkills.Count, ((ObservableCollection<cEVESkill>)value).Count);

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }

    public class GroupSkillPointsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            if ((int)value != 0)
            {
                return $"{((int)value).ToString("##,#")} Points";
            }
            else
            {
                return "0 Points";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SkillSkillPointsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            if ((int)value != 0)
            {
                return $"SP: {((int)value).ToString("##,#")}";
            }
            else
            {
                return "SP: 0";
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MaxSkillPointsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            List<double> SkillPointsPerLevel = value as List<double>;

            return SkillPointsPerLevel[4].ToString("##,#");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SkillProgressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            //cEVESkill thisSkill = value as cEVESkill;

            //if (thisSkill.characterSkill != null)
            //{
            //    if (thisSkill.characterSkill.level == 5)
            //        return 100;

            //    if (thisSkill.characterSkill.skillpoints > thisSkill.skillPointsPerLevel[thisSkill.characterSkill.level - 1])
            //    {
            //        double pointsDifferent = thisSkill.characterSkill.skillpoints - thisSkill.skillPointsPerLevel[thisSkill.characterSkill.level - 1];
            //        double pointsToNextLevel = thisSkill.skillPointsPerLevel[thisSkill.characterSkill.level] - thisSkill.skillPointsPerLevel[thisSkill.characterSkill.level - 1];

            //        return (pointsDifferent / pointsToNextLevel) * 100;
            //    }
            //    else
            //        return 0;
            //}
            //else
                return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }

    public class AttributeIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            //if (value != null)
            //{
            //    switch ((Enums.Attributes)value)
            //    {
            //        case Enums.Attributes.Intelligence:
            //            return new BitmapImage(new Uri("ms-appx:///Assets/Images/intelligence.png", UriKind.Absolute));
            //        case Enums.Attributes.Charisma:
            //            return new BitmapImage(new Uri("ms-appx:///Assets/Images/charisma.png", UriKind.Absolute));
            //        case Enums.Attributes.Memory:
            //            return new BitmapImage(new Uri("ms-appx:///Assets/Images/memory.png", UriKind.Absolute));
            //        case Enums.Attributes.Perception:
            //            return new BitmapImage(new Uri("ms-appx:///Assets/Images/perception.png", UriKind.Absolute));
            //        case Enums.Attributes.Willpower:
            //            return new BitmapImage(new Uri("ms-appx:///Assets/Images/willpower.png", UriKind.Absolute));
            //        default:
            //            return null;
            //    }
            //}

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}
