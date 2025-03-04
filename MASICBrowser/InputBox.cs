﻿using System;
using System.Windows.Forms;

namespace MASICBrowser
{
    /// <summary>
    /// The InputBox class is used to show a prompt in a dialog box using the static method Show().
    /// </summary>
    /// <remarks>
    /// <para>
    /// https://www.reflectionit.nl/blog/2003/c-inputbox
    /// Copyright © 2003 Reflection IT
    /// </para>
    /// <para>
    /// This software is provided 'as-is', without any express or implied warranty.
    /// In no event will the authors be held liable for any damages arising from the
    /// use of this software.
    /// </para>
    /// <para>
    /// Permission is granted to anyone to use this software for any purpose,
    /// including commercial applications, subject to the following restrictions:
    /// </para>
    /// <para>
    /// 1. The origin of this software must not be misrepresented; you must not claim
    /// that you wrote the original software.
    /// </para>
    /// <para>
    /// 2. No substantial portion of the source code of this library may be redistributed
    /// without the express written permission of the copyright holders, where
    /// "substantial" is defined as enough code to be recognizably from this library.
    /// </para>
    /// </remarks>
    public sealed class InputBox : Form
    {
        // Ignore Spelling: Validator

        private Button buttonOK;
        private Button buttonCancel;
        private Label labelPrompt;
        private TextBox textBoxText;
        private ErrorProvider errorProviderText;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private readonly System.ComponentModel.Container components = null;

        private InputBox()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.labelPrompt = new System.Windows.Forms.Label();
            this.errorProviderText = new System.Windows.Forms.ErrorProvider();
            this.SuspendLayout();
            //
            // buttonOK
            //
            this.buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(288, 72);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += this.ButtonOK_Click;
            //
            // buttonCancel
            //
            this.buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.buttonCancel.CausesValidation = false;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(376, 72);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += this.ButtonCancel_Click;
            //
            // textBoxText
            //
            this.textBoxText.Location = new System.Drawing.Point(16, 32);
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.Size = new System.Drawing.Size(416, 20);
            this.textBoxText.TabIndex = 1;
            this.textBoxText.Text = "";
            this.textBoxText.Validating += this.textBoxText_Validating;
            this.textBoxText.TextChanged += this.textBoxText_TextChanged;
            //
            // labelPrompt
            //
            this.labelPrompt.AutoSize = true;
            this.labelPrompt.Location = new System.Drawing.Point(15, 15);
            this.labelPrompt.Name = "labelPrompt";
            this.labelPrompt.Size = new System.Drawing.Size(39, 13);
            this.labelPrompt.TabIndex = 0;
            this.labelPrompt.Text = "prompt";
            //
            // errorProviderText
            //
            this.errorProviderText.DataMember = null;
            //
            // InputBox
            //
            this.AcceptButton = this.buttonOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(464, 104);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.labelPrompt,
                                                                          this.textBoxText,
                                                                          this.buttonCancel,
                                                                          this.buttonOK});
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputBox";
            this.Text = "Title";
            this.ResumeLayout(false);
        }
        #endregion

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Validator = null;
            this.Close();
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Displays a prompt in a dialog box, waits for the user to input text or click a button.
        /// </summary>
        /// <param name="prompt">String expression displayed as the message in the dialog box</param>
        /// <param name="title">String expression displayed in the title bar of the dialog box</param>
        /// <param name="defaultResponse">String expression displayed in the text box as the default response</param>
        /// <param name="validator">Delegate used to validate the text</param>
        /// <param name="xPos">Numeric expression that specifies the distance of the left edge of the dialog box from the left edge of the screen.</param>
        /// <param name="yPos">Numeric expression that specifies the distance of the upper edge of the dialog box from the top of the screen</param>
        /// <returns>An InputBoxResult object with the Text and the OK property set to true when OK was clicked.</returns>
        public static InputBoxResult Show(string prompt, string title, string defaultResponse, InputBoxValidatingHandler validator, int xPos, int yPos)
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            using var form = new InputBox();

            form.labelPrompt.Text = prompt;
            form.Text = title;
            form.textBoxText.Text = defaultResponse;

            if (xPos >= 0 && yPos >= 0)
            {
                form.StartPosition = FormStartPosition.Manual;
                form.Left = xPos;
                form.Top = yPos;
            }
            form.Validator = validator;

            var result = form.ShowDialog();

            var returnValue = new InputBoxResult();
            if (result == DialogResult.OK)
            {
                returnValue.Text = form.textBoxText.Text;
                returnValue.OK = true;
            }
            return returnValue;
        }

        /// <summary>
        /// Displays a prompt in a dialog box, waits for the user to input text or click a button.
        /// </summary>
        /// <param name="prompt">String expression displayed as the message in the dialog box</param>
        /// <param name="title">String expression displayed in the title bar of the dialog box</param>
        /// <param name="defaultText">String expression displayed in the text box as the default response</param>
        /// <param name="validator">Delegate used to validate the text</param>
        /// <returns>An InputBoxResult object with the Text and the OK property set to true when OK was clicked.</returns>
        public static InputBoxResult Show(string prompt, string title, string defaultText, InputBoxValidatingHandler validator)
        {
            return Show(prompt, title, defaultText, validator, -1, -1);
        }

        /// <summary>
        /// Reset the ErrorProvider
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxText_TextChanged(object sender, EventArgs e)
        {
            errorProviderText.SetError(textBoxText, string.Empty);
        }

        /// <summary>
        /// Validate the Text using the Validator
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxText_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Validator != null)
            {
                var args = new InputBoxValidatingArgs { Text = textBoxText.Text };
                Validator(this, args);
                if (args.Cancel)
                {
                    e.Cancel = true;
                    errorProviderText.SetError(textBoxText, args.Message);
                }
            }
        }

        /// <summary>
        /// Delegate used to validate the object
        /// </summary>
        private InputBoxValidatingHandler Validator { get; set; }
    }

    /// <summary>
    /// Class used to store the result of an InputBox.Show message
    /// </summary>
    public class InputBoxResult
    {
        /// <summary>
        /// True if the user clicked OK
        /// </summary>
        public bool OK;

        /// <summary>
        /// Text entered by the user
        /// </summary>
        public string Text;
    }

    /// <summary>
    /// EventArgs used to Validate an InputBox
    /// </summary>
    public class InputBoxValidatingArgs : EventArgs
    {
        /// <summary>
        /// Text to parse (should contain an integer)
        /// </summary>
        public string Text;

        /// <summary>
        /// Error message to show the user if the number in Text could not be parsed
        /// </summary>
        public string Message;

        /// <summary>
        /// This is set to true
        /// </summary>
        public bool Cancel;
    }

    /// <summary>
    /// Delegate used to Validate an InputBox
    /// </summary>
    public delegate void InputBoxValidatingHandler(object sender, InputBoxValidatingArgs e);
}
