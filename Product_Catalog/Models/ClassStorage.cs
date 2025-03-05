using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassUnit;

namespace ClassStorage
{
    abstract class Storage
    {
        public abstract void SaveUnits(List<Unit> units);
        public abstract List<Unit> LoadUnits();
    }

    class StorageFromFile: Storage
    {
        private const string FileName = "catalog.bin";
        public override void SaveUnits(List<Unit> units)
        {
            //var json = JsonSerializer.Serialize(units, new JsonSerializerOptions { WriteIndented = true });
            //File.WriteAllText(FileName, json);
            using (FileStream fs = new FileStream(FileName, FileMode.Create))
            {
                using (BinaryWriter writer = new BinaryWriter(fs, Encoding.UTF8))
                {
                    writer.Write(units.Count);
                    foreach (var unit in units)
                    {
                        writer.Write(unit.Id);
                        writer.Write(unit.Name);
                        writer.Write(unit.Description);
                        writer.Write(unit.Price);
                        writer.Write(unit.Quantity);
                        writer.Write(unit.AddeDate.Ticks);
                        writer.Write(unit.QuantityHistory.Count);
                        foreach (var history in unit.QuantityHistory)
                        {
                            writer.Write(history);
                        }
                    }
                }
            }

        }
        public override List<Unit> LoadUnits()
        {
            List<Unit> units = new List<Unit>();
            if (File.Exists(FileName))
            {
                //var json = File.ReadAllText(FileName);
                //units = JsonSerializer.Deserialize<List<Unit>>(json);
                using (FileStream fs = new FileStream(FileName, FileMode.Open))
                {
                    using (BinaryReader reader = new BinaryReader(fs, Encoding.UTF8))
                    {
                        int count = reader.ReadInt32();
                        for (int i = 0; i < count; i++)
                        {
                            Unit unit = new Unit
                            {
                                Id = reader.ReadInt32(),
                                Name = reader.ReadString(),
                                Description = reader.ReadString(),
                                Price = reader.ReadDouble(),
                                Quantity = reader.ReadInt32(),
                                AddeDate = new DateTime(reader.ReadInt64()),
                            };
                            int historyCount = reader.ReadInt32();
                            for (int j = 0; j < historyCount; j++)
                            {
                                unit.QuantityHistory.Add(reader.ReadString());
                            }
                            units.Add(unit);
                        }
                    }
                }
            }
            return units;
        }
    }
}
