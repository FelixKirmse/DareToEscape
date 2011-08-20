using System;

namespace CodeEditor
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            EditorForm form = new EditorForm();
            form.Show();
            form.Editor = new Editor(form.editorOutput.Handle, form, form.editorOutput);
            form.Editor.Run();
        }
    }
#endif
}

