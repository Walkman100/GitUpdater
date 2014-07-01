'
' Created by SharpDevelop.
' User: Tristan
' Date: 7/1/2014
' Time: 8:02 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Partial Class Interval
	Inherits System.Windows.Forms.Form
	
	''' <summary>
	''' Designer variable used to keep track of non-visual components.
	''' </summary>
	Private components As System.ComponentModel.IContainer
	
	''' <summary>
	''' Disposes resources used by the form.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If components IsNot Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub
	
	''' <summary>
	''' This method is required for Windows Forms designer support.
	''' Do not change the method contents inside the source code editor. The Forms designer might
	''' not be able to load this method if it was changed manually.
	''' </summary>
	Private Sub InitializeComponent()
		Me.label1 = New System.Windows.Forms.Label()
		Me.lblInterval = New System.Windows.Forms.Label()
		Me.textBox1 = New System.Windows.Forms.TextBox()
		Me.SuspendLayout
		'
		'label1
		'
		Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.label1.Location = New System.Drawing.Point(12, 9)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(301, 35)
		Me.label1.TabIndex = 0
		Me.label1.Text = "Set GitUpdater to pull repo changes at a regular interval:"
		'
		'lblInterval
		'
		Me.lblInterval.Location = New System.Drawing.Point(13, 48)
		Me.lblInterval.Name = "lblInterval"
		Me.lblInterval.Size = New System.Drawing.Size(100, 16)
		Me.lblInterval.TabIndex = 1
		Me.lblInterval.Text = "Interval (Seconds):"
		'
		'textBox1
		'
		Me.textBox1.Location = New System.Drawing.Point(119, 45)
		Me.textBox1.Name = "textBox1"
		Me.textBox1.Size = New System.Drawing.Size(194, 20)
		Me.textBox1.TabIndex = 2
		AddHandler Me.textBox1.TextChanged, AddressOf Me.TextBox1_TextChanged
		'
		'Interval
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(325, 266)
		Me.Controls.Add(Me.textBox1)
		Me.Controls.Add(Me.lblInterval)
		Me.Controls.Add(Me.label1)
		Me.Name = "Interval"
		Me.Text = "REgular interval checker"
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private textBox1 As System.Windows.Forms.TextBox
	Private lblInterval As System.Windows.Forms.Label
	Private label1 As System.Windows.Forms.Label
End Class
