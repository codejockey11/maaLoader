using System;
using System.IO;
using System.IO.Compression;
using aviationLib;

namespace maaLoader
{
    class Program
    {
        static Char[] recordType_001_04 = new Char[04];
        static Char[] maaId_005_06 = new Char[06];

        static Char[] maaType_011_25 = new Char[25];
        static Char[] navaidIdentifier_036_04 = new Char[04];
        static Char[] navaidFacilityTypeCode_040_02 = new Char[02];
        static Char[] navaidFacilityTypeDescribed_042_25 = new Char[25];
        static Char[] azimuthFromNavaid_067_06 = new Char[06];
        static Char[] distance_073_06 = new Char[06];
        static Char[] navaidName_079_30 = new Char[30];
        static Char[] maaStateAbbreviation_109_02 = new Char[02];
        static Char[] maaStateName_111_30 = new Char[30];
        static Char[] maaAssociatedCityName_141_30 = new Char[30];
        static Char[] maaLatitude_171_14 = new Char[14];
        static Char[] maaLongitude_197_15 = new Char[15];
        static Char[] associatedAirportId_224_04 = new Char[04];
        static Char[] associatedAirportName_228_50 = new Char[50];
        static Char[] associatedAirportSiteNumber_278_11 = new Char[11];
        static Char[] nearestAirportId_289_04 = new Char[04];
        static Char[] nearestAirportDistance_293_06 = new Char[06];
        static Char[] nearestAirportDirection_299_02 = new Char[02];
        static Char[] maaAreaName_301_120 = new Char[120];
        static Char[] maaMaximumAltitude_421_08 = new Char[08];
        static Char[] maaMinimumAltitude_429_08 = new Char[08];
        static Char[] maaAreaRadius_437_05 = new Char[05];
        static Char[] showOnVfrChart_442_03 = new Char[03];
        static Char[] maaDescription_445_450 = new Char[450];
        static Char[] maaUse_895_08 = new Char[08];

        static Char[] maa2Latitude_011_14 = new Char[14];
        static Char[] maa2Longitude_037_15 = new Char[15];

        static Char[] timesOfUse_011_75 = new Char[75];

        static Char[] userGroupName_011_75 = new Char[75];

        static Char[] contactFacilityId_011_04 = new Char[04];
        static Char[] contactFacilityName_015_48 = new Char[48];
        static Char[] commercialFrequency_063_07 = new Char[07];
        static Char[] commercialChartFlag_070_01 = new Char[01];
        static Char[] militaryFrequency_071_07 = new Char[07];
        static Char[] militaryChartFlag_078_01 = new Char[01];

        static Char[] notams_011_04 = new Char[04];

        static Char[] remarks_011_300 = new Char[300];

        static StreamWriter ofileMAA1 = new StreamWriter("maabasedata.txt");
        static StreamWriter ofileMAA2 = new StreamWriter("maapolycoord.txt");
        static StreamWriter ofileMAA3 = new StreamWriter("maatimesofuse.txt");
        static StreamWriter ofileMAA4 = new StreamWriter("maausergroup.txt");
        static StreamWriter ofileMAA5 = new StreamWriter("maacontact.txt");
        static StreamWriter ofileMAA6 = new StreamWriter("maanotams.txt");
        static StreamWriter ofileMAA7 = new StreamWriter("maaremarks.txt");

        static StreamWriter ofileMAA8 = new StreamWriter("maaupdate.sql");

        static Boolean wasMissing = false;

        static void Main(string[] args)
        {

            String userprofileFolder = Environment.GetEnvironmentVariable("USERPROFILE");
            String[] fileEntries = Directory.GetFiles(userprofileFolder + "\\Downloads\\", "28DaySubscription*.zip");

            ZipArchive archive = ZipFile.OpenRead(fileEntries[0]);
            ZipArchiveEntry entry = archive.GetEntry("MAA.txt");
            entry.ExtractToFile("MAA.txt", true);

            StreamReader file = new StreamReader("MAA.txt");

            ofileMAA8.Write("USE aviation;");
            ofileMAA8.Write(ofileMAA8.NewLine);

            String rec = file.ReadLine();

            while (!file.EndOfStream)
            {
                ProcessRecord(rec);

                rec = file.ReadLine();
            }

            ProcessRecord(rec);

            file.Close();

            ofileMAA1.Close();
            ofileMAA2.Close();
            ofileMAA3.Close();
            ofileMAA4.Close();
            ofileMAA5.Close();
            ofileMAA6.Close();
            ofileMAA7.Close();
            ofileMAA8.Close();

        }

        static void ProcessRecord(String record)
        {
            recordType_001_04 = record.ToCharArray(0, 4);
            maaId_005_06 = record.ToCharArray(4, 6);

            String rt = new String(recordType_001_04);
            Int32 r = String.Compare(rt, "MAA1");
            if (r == 0)
            {
                wasMissing = false;

                String s = new String(maaId_005_06).Trim();
                ofileMAA1.Write(s);
                ofileMAA1.Write('~');

                maaType_011_25 = record.ToCharArray(10, 25);
                s = new String(maaType_011_25).Trim();
                ofileMAA1.Write(s);
                ofileMAA1.Write('~');

                navaidIdentifier_036_04 = record.ToCharArray(35, 4);
                s = new String(navaidIdentifier_036_04).Trim();
                ofileMAA1.Write(s);
                ofileMAA1.Write('~');

                navaidFacilityTypeCode_040_02 = record.ToCharArray(39, 2);
                s = new String(navaidFacilityTypeCode_040_02).Trim();
                ofileMAA1.Write(s);
                ofileMAA1.Write('~');

                navaidFacilityTypeDescribed_042_25 = record.ToCharArray(41, 25);
                s = new String(navaidFacilityTypeDescribed_042_25).Trim();
                ofileMAA1.Write(s);
                ofileMAA1.Write('~');

                azimuthFromNavaid_067_06 = record.ToCharArray(66, 6);
                s = new String(azimuthFromNavaid_067_06).Trim();
                ofileMAA1.Write(s);
                ofileMAA1.Write('~');

                distance_073_06 = record.ToCharArray(72, 6);
                s = new String(distance_073_06).Trim();
                ofileMAA1.Write(s);
                ofileMAA1.Write('~');

                navaidName_079_30 = record.ToCharArray(78, 6);
                s = new String(navaidName_079_30).Trim();
                ofileMAA1.Write(s);
                ofileMAA1.Write('~');

                maaStateAbbreviation_109_02 = record.ToCharArray(108, 2);
                s = new String(maaStateAbbreviation_109_02).Trim();
                ofileMAA1.Write(s);
                ofileMAA1.Write('~');

                maaStateName_111_30 = record.ToCharArray(110, 30);
                s = new String(maaStateName_111_30).Trim();
                ofileMAA1.Write(s);
                ofileMAA1.Write('~');

                maaAssociatedCityName_141_30 = record.ToCharArray(140, 30);
                s = new String(maaAssociatedCityName_141_30).Trim();
                ofileMAA1.Write(s);
                ofileMAA1.Write('~');

                maaLatitude_171_14 = record.ToCharArray(170, 14);
                maaLongitude_197_15 = record.ToCharArray(196, 15);

                String ll1 = new String(maaLatitude_171_14).Trim();
                String ll2 = new String(maaLongitude_197_15).Trim();

                if (ll1.Length > 0 && ll2.Length > 0)
                {
                    LatLon ll = new LatLon(ll1, ll2);
                    ofileMAA1.Write(ll.formattedLat);
                    ofileMAA1.Write('~');

                    ofileMAA1.Write(ll.formattedLon);
                    ofileMAA1.Write('~');
                }
                else
                {
                    ofileMAA1.Write('~');
                    ofileMAA1.Write('~');

                    wasMissing = true;
                }

                associatedAirportId_224_04 = record.ToCharArray(223, 4);
                s = new String(associatedAirportId_224_04).Trim();
                ofileMAA1.Write(s);
                ofileMAA1.Write('~');

                associatedAirportName_228_50 = record.ToCharArray(227, 50);
                s = new String(associatedAirportName_228_50).Trim();
                ofileMAA1.Write(s);
                ofileMAA1.Write('~');

                associatedAirportSiteNumber_278_11 = record.ToCharArray(277, 10);
                s = new String(associatedAirportSiteNumber_278_11).Trim();
                ofileMAA1.Write(s);
                ofileMAA1.Write('~');

                nearestAirportId_289_04 = record.ToCharArray(288, 4);
                s = new String(nearestAirportId_289_04).Trim();
                ofileMAA1.Write(s);
                ofileMAA1.Write('~');

                nearestAirportDistance_293_06 = record.ToCharArray(292, 6);
                s = new String(nearestAirportDistance_293_06).Trim();
                ofileMAA1.Write(s);
                ofileMAA1.Write('~');

                nearestAirportDirection_299_02 = record.ToCharArray(298, 2);
                s = new String(nearestAirportDirection_299_02).Trim();
                ofileMAA1.Write(s);
                ofileMAA1.Write('~');

                maaAreaName_301_120 = record.ToCharArray(300, 120);
                s = new String(maaAreaName_301_120).Trim();
                ofileMAA1.Write(s);
                ofileMAA1.Write('~');

                maaMaximumAltitude_421_08 = record.ToCharArray(420, 8);
                s = new String(maaMaximumAltitude_421_08).Trim();
                ofileMAA1.Write(s);
                ofileMAA1.Write('~');

                maaMinimumAltitude_429_08 = record.ToCharArray(428, 8);
                s = new String(maaMinimumAltitude_429_08).Trim();
                ofileMAA1.Write(s);
                ofileMAA1.Write('~');

                maaAreaRadius_437_05 = record.ToCharArray(436, 5);
                s = new String(maaAreaRadius_437_05).Trim();
                ofileMAA1.Write(s);
                ofileMAA1.Write('~');

                showOnVfrChart_442_03 = record.ToCharArray(441, 3);
                s = new String(showOnVfrChart_442_03).Trim();
                ofileMAA1.Write(s);
                ofileMAA1.Write('~');

                maaDescription_445_450 = record.ToCharArray(444, 450);
                s = new String(maaDescription_445_450).Trim();
                ofileMAA1.Write(s);
                ofileMAA1.Write('~');

                maaUse_895_08 = record.ToCharArray(894, 8);
                s = new String(maaUse_895_08).Trim();
                ofileMAA1.Write(s);
                ofileMAA1.Write(ofileMAA1.NewLine);
            }

            r = String.Compare(rt, "MAA2");
            if (r == 0)
            {
                String s = new String(maaId_005_06).Trim();
                ofileMAA2.Write(s);
                ofileMAA2.Write('~');

                maa2Latitude_011_14 = record.ToCharArray(10, 14);
                maa2Longitude_037_15 = record.ToCharArray(36, 15);

                LatLon ll = new LatLon(new String(maa2Latitude_011_14).Trim(), new String(maa2Longitude_037_15).Trim());
                ofileMAA2.Write(ll.formattedLat);
                ofileMAA2.Write('~');

                ofileMAA2.Write(ll.formattedLon);
                ofileMAA2.Write(ofileMAA1.NewLine);

                if (wasMissing == true)
                {
                    wasMissing = false;

                    ofileMAA8.Write("UPDATE maabasedata SET latitude='" + ll.formattedLat + "', longitude='" + ll.formattedLon + "' WHERE maaId='" + new String(maaId_005_06).Trim() + "';");
                    ofileMAA8.Write(ofileMAA1.NewLine);
                }
            }

            r = String.Compare(rt, "MAA3");
            if (r == 0)
            {
                String s = new String(maaId_005_06).Trim();
                ofileMAA3.Write(s);
                ofileMAA3.Write('~');


                timesOfUse_011_75 = record.ToCharArray(10, 75);
                s = new String(timesOfUse_011_75).Trim();
                ofileMAA3.Write(s);
                ofileMAA3.Write(ofileMAA3.NewLine);
            }

            r = String.Compare(rt, "MAA4");
            if (r == 0)
            {
                String s = new String(maaId_005_06).Trim();
                ofileMAA4.Write(s);
                ofileMAA4.Write('~');

                userGroupName_011_75 = record.ToCharArray(10, 75);
                s = new String(userGroupName_011_75).Trim();
                ofileMAA4.Write(s);
                ofileMAA4.Write(ofileMAA3.NewLine);
            }

            r = String.Compare(rt, "MAA5");
            if (r == 0)
            {
                String s = new String(maaId_005_06).Trim();
                ofileMAA5.Write(s);
                ofileMAA5.Write('~');

                contactFacilityId_011_04 = record.ToCharArray(10, 4);
                s = new String(contactFacilityId_011_04).Trim();
                ofileMAA5.Write(s);
                ofileMAA5.Write('~');

                contactFacilityName_015_48 = record.ToCharArray(14, 48);
                s = new String(contactFacilityName_015_48).Trim();
                ofileMAA5.Write(s);
                ofileMAA5.Write('~');

                commercialFrequency_063_07 = record.ToCharArray(62, 7);
                s = new String(commercialFrequency_063_07).Trim();
                ofileMAA5.Write(s);
                ofileMAA5.Write('~');

                commercialChartFlag_070_01 = record.ToCharArray(69, 1);
                s = new String(commercialChartFlag_070_01).Trim();
                ofileMAA5.Write(s);
                ofileMAA5.Write('~');

                militaryFrequency_071_07 = record.ToCharArray(70, 7);
                s = new String(militaryFrequency_071_07).Trim();
                ofileMAA5.Write(s);
                ofileMAA5.Write('~');

                militaryChartFlag_078_01 = record.ToCharArray(77, 1);
                s = new String(militaryChartFlag_078_01).Trim();
                ofileMAA5.Write(s);
                ofileMAA5.Write(ofileMAA5.NewLine);
            }

            r = String.Compare(rt, "MAA6");
            if (r == 0)
            {
                String s = new String(maaId_005_06).Trim();
                ofileMAA6.Write(s);
                ofileMAA6.Write('~');

                notams_011_04 = record.ToCharArray(10, 4);
                s = new String(notams_011_04).Trim();
                ofileMAA6.Write(s);
                ofileMAA6.Write(ofileMAA6.NewLine);
            }

            r = String.Compare(rt, "MAA7");
            if (r == 0)
            {
                String s = new String(maaId_005_06).Trim();
                ofileMAA7.Write(s);
                ofileMAA7.Write('~');

                remarks_011_300 = record.ToCharArray(10, 300);
                s = new String(remarks_011_300).Trim();
                ofileMAA7.Write(s);
                ofileMAA7.Write(ofileMAA7.NewLine);
            }

        }

    }

}