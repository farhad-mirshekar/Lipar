using System.Runtime.CompilerServices;

namespace Lipar.Core.Infrastructure
{
    public class EngineContext
    {
        #region Methods

        /// <summary>
        /// Create a static instance of the Nop engine.
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ILiparEngine Create()
        {
            //create NopEngine as engine
            return Singleton<ILiparEngine>.Instance ?? (Singleton<ILiparEngine>.Instance = new LiparEngine());
        }

        /// <summary>
        /// Sets the static engine instance to the supplied engine. Use this method to supply your own engine implementation.
        /// </summary>
        /// <param name="engine">The engine to use.</param>
        /// <remarks>Only use this method if you know what you're doing.</remarks>
        public static void Replace(ILiparEngine engine)
        {
            Singleton<ILiparEngine>.Instance = engine;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton Nop engine used to access Nop services.
        /// </summary>
        public static ILiparEngine Current
        {
            get
            {
                if (Singleton<ILiparEngine>.Instance == null)
                {
                    Create();
                }

                return Singleton<ILiparEngine>.Instance;
            }
        }

        #endregion
    }
}
