namespace DES
{
    internal class DecryptDES
    {
        public static void Decrypt()
        {
            Global.outputText = "";
            Global.binaryBlocksIP = DES.InitialPermutation(Global.binaryBlocksIP);
            for (int i = 0; i < Global.numberOfBlocks; i++)
            {
                string[] L = new string[Global.quantityOfRounds + 1]; // массивы, которы хранят левую и правую части блока
                string[] R = new string[Global.quantityOfRounds + 1]; // соответственно
                for (int j = Global.quantityOfRounds + 1; j > 0; j--)
                {
                    if (i == 0)
                    {
                        if (j == Global.quantityOfRounds + 1)
                        {
                            R[j - 1] = Global.binaryBlocksIP[i][..(Global.sizeOfBlock / 2)];
                            L[j - 1] = Global.binaryBlocksIP[i].Substring(Global.sizeOfBlock / 2, Global.sizeOfBlock / 2);
                        }
                        else
                        {
                            R[j - 1] = L[j];
                            L[j - 1] = DES.XOR(R[j], DES.F(L[j], Global.K[j]));
                        }
                    }
                    else
                    {
                        if (j == Global.quantityOfRounds + 1)
                        {
                            R[j - 1] = Global.binaryBlocksIP[i][..(Global.sizeOfBlock / 2)];
                            L[j - 1] = Global.binaryBlocksIP[i].Substring(Global.sizeOfBlock / 2, Global.sizeOfBlock / 2);
                        }
                        else
                        {
                            R[j - 1] = L[j];
                            L[j - 1] = DES.XOR(R[j], DES.F(L[j], Global.K[j]));
                        }
                    }
                }
                Global.binaryBlocksIP[i] = L[0] + R[0];
            }
            Global.binaryBlocksIP = DES.InitialPermutation1(Global.binaryBlocks);
            Global.binaryBlocksIP = DES.InitialPermutation(Global.binaryBlocks);
            foreach (var i in Global.binaryBlocksIP)
                Global.outputText += DES.BinaryToStringFormat(i);
        }
    }
}
