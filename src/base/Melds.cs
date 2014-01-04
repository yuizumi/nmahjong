namespace NMahjong.Base
{
    /**
      <summary>
        Provides extension methods for <see cref="Meld"/>.
      </summary>
    */
    public static class Melds
    {
        /**
          <summary>
            Gets a boolean value that represents whether the specified meld is open.
          </summary>
          <param name="meld">
            The meld to investigate.
          </param>
          <returns>
            <see langword="true"/> if <paramref name="meld"/> represents an open meld;
            otherwise, <see langword="false"/>.
          </returns>
        */
        public static bool IsOpen(this Meld meld)
        {
            return meld.State == MeldState.Open;
        }
    }
}
