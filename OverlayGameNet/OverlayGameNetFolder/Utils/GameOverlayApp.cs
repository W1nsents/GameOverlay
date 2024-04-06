using GameOverlay.Drawing;
using GameOverlay.Windows;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverlayGameNet.Utils
{
    internal class GameOverlayApp : IDisposable
    {
        private readonly GraphicsWindow _window;

        private readonly Dictionary<string, SolidBrush> _brushes;
        private readonly Dictionary<string, Font> _fonts;
        private readonly Dictionary<string, Image> _images;

        private GameOverlay.Drawing.Geometry _gridGeometry;
        private System.Drawing.Rectangle _gridBounds;

        public GameOverlayApp()
        {
            _brushes = new Dictionary<string, SolidBrush>();
            _fonts = new Dictionary<string, Font>();
            _images = new Dictionary<string, Image>();

            var gfx = new Graphics()
            {
                MeasureFPS = true,
                PerPrimitiveAntiAliasing = true,
                TextAntiAliasing = true,
                VSync = true,
                Width = 1920,
                Height = 1080,
                WindowHandle = Configuration.handle
            };

            _window = new GraphicsWindow(0, 0, 1920, 1080, gfx)
            {
                FPS = 5,
                IsTopmost = true,
                IsVisible = true,
            };

            _window.DrawGraphics += _window_DrawGraphics;
            _window.SetupGraphics += _window_SetupGraphics;
        }

        private void _window_SetupGraphics(object sender, SetupGraphicsEventArgs e)
        {
            var gfx = e.Graphics;

            if (e.RecreateResources)
            {
                foreach (var pair in _brushes) pair.Value.Dispose();
                foreach (var pair in _images) pair.Value.Dispose();
            }

            _brushes["black"] = gfx.CreateSolidBrush(0, 0, 0);
            _brushes["white"] = gfx.CreateSolidBrush(255, 255, 255);
            _brushes["red"] = gfx.CreateSolidBrush(255, 0, 0);
            _brushes["green"] = gfx.CreateSolidBrush(0, 255, 0);
            _brushes["blue"] = gfx.CreateSolidBrush(0, 0, 255);
            _brushes["background"] = gfx.CreateSolidBrush(0x33, 0x36, 0x3F);
            _brushes["grid"] = gfx.CreateSolidBrush(0, 0, 0, 0.5f);
            _brushes["random"] = gfx.CreateSolidBrush(0, 0, 0);

            if (e.RecreateResources) return;

            _fonts["arial"] = gfx.CreateFont("Arial", 12);
            _fonts["arial bold"] = gfx.CreateFont("Arial Bold", 18);

            _gridGeometry = gfx.CreateGeometry();

            for (float x = _gridBounds.Left; x <= _gridBounds.Right; x += 20)
            {
                var line = new Line(x, _gridBounds.Top, x, _gridBounds.Bottom);
                _gridGeometry.BeginFigure(line);
                _gridGeometry.EndFigure(false);
            }

            for (float y = _gridBounds.Top; y <= _gridBounds.Bottom; y += 20)
            {
                var line = new Line(_gridBounds.Left, y, _gridBounds.Right, y);
                _gridGeometry.BeginFigure(line);
                _gridGeometry.EndFigure(false);
            }
        }

        private void _window_DrawGraphics(object sender, DrawGraphicsEventArgs e)
        {
            var gfx = e.Graphics;

            gfx.ClearScene();
            if (Application.OpenForms["OverlayForm"] == null)
            {
                Task.Run(() => new OverlayForm().ShowDialog());
            }
            else
            {
                gfx.ClearScene();
            }
        }

        public void Run()
        {
            _window.Create();
            _window.Join();
        }

        ~GameOverlayApp()
        {
            Dispose(false);
        }

        #region IDisposable Support
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                _window.Dispose();

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
