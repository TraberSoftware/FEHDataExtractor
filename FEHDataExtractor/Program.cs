﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FEHDataExtractorLib;

namespace FEHDataExtractor
{
    static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(
                new GCWorld(), 
                new BaseExtractArchive<SinglePerson>(), 
                new BaseExtractArchive<SingleEnemy>(), 
                new GenericText("", CommonRelated.Common),
                new BaseExtractArchive<SingleSkill>(), 
                new BaseExtractArchive<Quest_group>(), 
                new Decompress(), 
                new BaseExtractArchive<TempestTrial>(), 
                new Messages(), 
                new WeaponClasses(), 
                new BaseExtractArchiveDirect<Forging_Bonds>()
            ));
        }
    }
}
