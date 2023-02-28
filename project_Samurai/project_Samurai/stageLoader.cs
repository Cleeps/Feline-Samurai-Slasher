using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace project_Samurai
{
    class StageLoader
    {
        // Fields
        StreamReader reader;
        Tile[,] tileLayout;
        char[,] levelLayout;
        Texture2D tileTextures;

        // Properties
        public Tile[,] TileLayout
        {
            get { return tileLayout; }
        }

        // Constructor
        public StageLoader(Texture2D texts)
        {
            tileTextures = texts;
        }

        // Methods
        public void LoadLevel(int levelNumber)
        {
            // Try to open the file
            try
            {
                // Open the file
                reader = new StreamReader(string.Format("..//..//..//levels//level{0}.txt", levelNumber));
                string[] arenaSize = new string[2];
                string line;

                // Set the stage size from the file
                line = reader.ReadLine();

                arenaSize = line.Split(',');
                tileLayout = new Tile[int.Parse(arenaSize[1]), int.Parse(arenaSize[0])];

                // Counting Variable
                int i = 0;
                int j = 0;

                while ((line = reader.ReadLine()) != null) 
                {
                    j = 0;

                    foreach (char c in line)
                    {
                        switch (line[j])
                        {
                            case 't':
                                tileLayout[i, j] = new Tile(new Rectangle(j * 32, i * 32, 32, 32), tileTextures);
                                break;

                            case ' ':
                                tileLayout[i, j] = null;
                                break;
                        }

                        j++;
                    }

                    i++;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            // Close the stream if it has been opened
            if (reader != null)
            {
                reader.Close();
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            for (int i = 0; i < tileLayout.GetLength(0); i++)
            {
                for (int j = 0; j < tileLayout.GetLength(1); j++)
                {
                    if (tileLayout[i, j] != null)
                    {
                        _spriteBatch.Draw(tileLayout[i, j].Texture, new Vector2(tileLayout[i, j].TileX, tileLayout[i, j].TileY), Color.White);
                    }
                }
            }
        }
    }
}
