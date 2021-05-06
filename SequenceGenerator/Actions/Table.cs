using System;

namespace SequenceGenerator.Actions
{
    class Table : JsonAction
    {
        public string ModifierCheck = "";

        public Roll dicecheck;

        public Result[] results;

        public Table()
        {
            ActionType = "RollTable";
        }
        
        public override bool Validate()
        {
            int MinLength = dicecheck.GetRangeDifference();

            int length = 0;
            foreach(Result r in results)
            {
                length += r.Weight;
            }

            return MinLength <= length;
        }

        Result ToResult(int id)
        {
            int i = 0;
            int r = 0;
            while(i < id)
            {
                i += results[r].Weight;
                r++;
            }
            r--;

            return results[r];
        }

        public override void ApplyResult(ref Creation creation)
        {
            if(ModifierCheck != null)
            {
                int bonus;
                creation.GetValue(ModifierCheck, out bonus);
                dicecheck.bonus = bonus;
            }

            int rollValue = dicecheck.PerformRoll();

            Result result = ToResult(rollValue);

            if(result.Condition != null)
            {
                Console.WriteLine($"Checking for condition:{result.Condition}");
                int setValue;
                //do condition check.
                if(result.Condition.StartsWith("::")) //this means that we grab not from the creation, but from settings.
                {
                    if(SequenceSettings.ActiveSettings.GenSettings.ContainsKey(result.Condition.Remove(0,2)))
                    {
                        setValue = SequenceSettings.ActiveSettings.GenSettings[result.Condition.Remove(0, 2)];
                    }
                    else
                    {
                        setValue = 0;
                    }
                }
                else
                {
                    creation.GetValue(result.Condition, out setValue); //already has some of the checks built in.
                }

                if(setValue == result.ConditionTarget)
                {
                    Console.WriteLine($"Condition detected, changing results from {rollValue} to {result.NewResult}");
                    result = ToResult(result.NewResult); //conditions were confirmed, results have been changed.
                }

            }

            if(result.Description != null)
                creation.AddTableResult(result.Description);

            //apply result to creation (if expected.)
            if (result.Items != null)
            {
                foreach (var item in result.Items)
                {

                    if (item.Target != null)
                    {
                        creation.GetValue(item.Target, out int value);
                        switch (item.Action)
                        {
                            case Operation.ADD:
                                creation.SetValue(item.Target, item.Value + value);
                                break;
                            case Operation.SUB:
                                creation.SetValue(item.Target, value - item.Value);
                                break;
                            case Operation.MULTI:
                                creation.SetValue(item.Target, value * item.Value);
                                break;
                            case Operation.DIV:
                                creation.SetValue(item.Target, value / item.Value);
                                break;
                            case Operation.SET:
                            default:
                                creation.SetValue(item.Target, item.Value); //default operation is SET.
                                break;
                        }
                    }

                    if (result.TargetTable != null)
                    {
                        JsonAction target = JsonController.GetJsonAction(result.TargetTable);
                        if (target != null)
                            target.ApplyResult(ref creation);

                    }
                }
            }
        }

        public override object CreateDummyAction()
        {
            Table table = new Table
            {
                dicecheck = new Roll
                {
                    Die = Dice.d20
                },
                ModifierCheck = "CultureMod",
                results = new Result[5]
            };

            table.results[0] = new Result
            {
                Weight = 1,
                Description = "You had a bad fall.  Add an injury and an incident.",
                TargetTable = "Injury",
                Condition = "::NoExtriemes",
                ConditionTarget = 1,
                NewResult = 2
                
            };
            table.results[0].Items = new ValueOperation[2];
            table.results[0].Items[0] = new ValueOperation
            {
                Target = "Injury",
                Value = 1
            };
            table.results[0].Items[1] = new ValueOperation
            {
                Target = "Incident",
                Value = 1
            };


            table.results[1] = new Result
            {
                Weight = 6,
                Description = "You performed poorly.",
                TargetTable = "Unlucky"
            };

            table.results[2] = new Result
            {
                Weight = 6,
                Description = "This was an average performance."
            };

            table.results[3] = new Result()
            {
                Weight = 6,
                Description = "This is an above average performance",
                TargetTable = "Boon"
            };

            table.results[4] = new Result()
            {
                Weight = 1,
                Description = "This is a glorious performance",
                TargetTable = "Boon",
                Condition = "::NoExtriemes",
                ConditionTarget = 1,
                NewResult = 19
            };
            table.results[4].Items = new ValueOperation[1];
            table.results[4].Items[0] = new ValueOperation
            {
                Target = "Boon",
                Action = Operation.SET,
                Value = 2
            };

            return table;
        }
    }


}
