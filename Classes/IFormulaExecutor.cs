namespace BerlinClock.Classes
{
    public interface IFormulaExecutor
    {
        (int result, int formulaValue, string specialCharacter) Execute(int time, string formula);
    }
}
