﻿namespace IoneVectronConverter.Vectron.MasterData
{
    public class SelWin
    {
        public int SelectCompulsion { get; set; }
        public string[] PLUs { get; set; }
        public int[] PLUNos => PLUs.Where(x => !x.StartsWith("*")).Select(x => Convert.ToInt32(x)).ToArray();
        public int SelectCountIone => SelectCount == 0 ? 1 : SelectCount;
        public int SelectCount { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public bool ZeroPriceAllowed { get; set; }
    }
}
