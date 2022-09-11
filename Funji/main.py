import tkinter as tk
from tkinter import ttk
from tkinter import filedialog


class Window:

    ws = tk.Tk()
    text_zone = tk.Text(ws)
    text_zone.pack(expand=tk.YES, fill=tk.BOTH, side=tk.LEFT)

    def file1(self):
        if not self.text_zone.edit_modified():
            self.text_zone.delete('1.0', tk.END)
        else:
            self.savefileas()

            self.text_zone.delete('1.0', tk.END)

        self.text_zone.edit_modified(0)

        self.ws.title('Nuevo documento*')

    def openfile(self):

        if not self.text_zone.edit_modified():
            try:
                path = filedialog.askopenfile(filetypes=(
                    ("Text files", "*.txt"), ("All files", "*.*"))).name

                self.ws.title('Fuji - ' + path)

                with open(path, 'r') as f:
                    content = f.read()
                    self.text_zone.delete('1.0', tk.END)
                    self.text_zone.insert('1.0', content)

                    self.text_zone.edit_modified(0)

            except:
                pass

        else:
            self.savefileas()

            self.text_zone.edit_modified(0)
            self.openfile()

    def savefile(self):
        try:

            path = self.ws.title().split('-')[1][1:]

        except:
            path = ''

        if path != '':

            with open(path, 'w') as f:
                content = self.text_zone.get('1.0', tk.END)
                f.write(content)

        else:
            self.savefileas()

        self.text_zone.edit_modified(0)

    def savefileas(self):
        try:
            path = filedialog.asksaveasfile(filetypes=(
                ("Text files", "*.txt"), ("All files", "*.*"))).name
            self.ws.title('Fuji - ' + path)

        except:
            return

        with open(path, 'w') as f:
            f.write(self.text_zone.get('1.0', tk.END))

    def initWindow(self):
        self.ws.title('Fuji')
        self.ws.geometry('800x600')

        menubar = tk.Menu(self.ws)

        # Nuevo
        filemenu = tk.Menu(menubar, tearoff=0)
        menubar.add_cascade(label="Nuevo", command=self.file1)

        # Opciones
        fileoptions = tk.Menu(menubar, tearoff=0)
        fileoptions.add_command(label="Abrir", command=self.openfile)
        fileoptions.add_command(label="Guardar", command=self.savefile)
        fileoptions.add_command(label="Guardar como...",
                                command=self.savefileas)
        fileoptions.add_separator()
        menubar.add_cascade(label="Opciones", menu=fileoptions)

        # Salir
        fileoptions = tk.Menu(menubar, tearoff=0)
        menubar.add_cascade(label="Salir", command=self.ws.quit)

        scrollbar = ttk.Scrollbar(
            self.ws, orient=tk.VERTICAL, command=self.text_zone.yview)
        scrollbar.pack(fill=tk.Y, side=tk.RIGHT)
        self.text_zone['yscrollcommand'] = scrollbar.set

        self.ws.config(menu=menubar)

        self.ws.mainloop()


if __name__ == '__main__':
    window = Window()
    window.initWindow()
