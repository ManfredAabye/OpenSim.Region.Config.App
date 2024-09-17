import tkinter as tk
from tkinter import messagebox, filedialog
import configparser
import uuid
import math
import random

# Erweiterte Listen mit Vorsilben und Nachsilben
VORSILBE = [
	"Alt", "Augs", "Ber", "Biele", "Dort", "Dres", "Duel", "Er", "Frank", "Gos",
	"Gör", "Heil", "Hild", "Jena", "Kass", "Kiel", "Köln", "Linz", "Magde", "Mann",
	"Mar", "Mittel", "Neu", "Nord", "Ober", "Osna", "Ost", "Reut", "Rost", "Sieger",
	"Stutt", "Sued", "Unter", "West", "Neub", "Stein", "Zell", "Trier", "Hagen", "Wert"
]

NACHSILBE = [
	"markt", "burg", "lin", "feld", "mund", "den", "len", "men", "furt", "lar",
	"litz", "bronn", "heim", "er", "el", "ken", "Rhein", "Delhi", "Hessen", "Bayern",
	"Pfalz", "brück", "land", "Harz", "lingen", "ock", "land", "gart", "Baden", 
	"Tirol", "Franken", "Sachsen", "Falen", "ner", "stein", "stadt", "bach", "tal", 
	"hof", "en", "ring"
]

def generate_random_name():
	# Wählt zufällig eine Vorsilbe und eine Nachsilbe aus
	vorsilbe = random.choice(VORSILBE)
	nachsilbe = random.choice(NACHSILBE)
	
	# Generiert den Namen durch Verknüpfung der Vorsilbe und Nachsilbe
	name = vorsilbe + nachsilbe
	return name

# Custom ConfigParser class to preserve key case sensitivity
class CaseConfigParser(configparser.ConfigParser):
	def optionxform(self, optionstr):
		return optionstr  # Keep original case

class RegionConfigApp:
	def __init__(self, root):
		self.root = root
		self.index = 0

		# Set window icon
		self.root.iconbitmap("icon.ico")

		# Set window title
		self.root.title("Region Configurations")

		# Set window size
		self.root.geometry("450x700")
		
		# Initialize variables with default values
		self.region_name = tk.StringVar(value=generate_random_name())
		self.region_uuid = tk.StringVar(value=str(uuid.uuid4()))
		self.maptile_uuid = tk.StringVar(value=self.region_uuid.get())
		
		# Location variable for grid position
		self.location = tk.StringVar(value="1000,1000")

		self.size = tk.IntVar(value=256)
		self.internal_port = tk.IntVar(value=9050)
		self.external_host = tk.StringVar(value="SYSTEMIP")
		self.max_prims = tk.IntVar(value=100000)
		self.max_agents = tk.IntVar(value=99)
		self.internal_address = tk.StringVar(value="0.0.0.0")
		self.allow_alt_ports = tk.BooleanVar(value=False)
		self.non_physical_prim_max = tk.IntVar(value=256)
		self.physical_prim_max = tk.IntVar(value=64)

		# New variables for additional settings
		self.clamp_prim_size = tk.BooleanVar(value=False)
		self.max_prims_per_user = tk.IntVar(value=-1)
		self.scope_id = tk.StringVar(value=self.region_uuid.get())
		self.region_type = tk.StringVar(value="")        
		self.render_min_height = tk.IntVar(value=-1)
		self.render_max_height = tk.IntVar(value=100)
		self.maptile_static_file = tk.StringVar(value="SomeFile.png")
		self.master_avatar_first_name = tk.StringVar(value="John")
		self.master_avatar_last_name = tk.StringVar(value="Doe")
		self.master_avatar_sandbox_password = tk.StringVar(value="passwd")

		self.region_count = 0  # Start at 0
		self.angle = 0
		self.radius = 1  # Initial radius for spirals
		self.locations = []  # List to store locations of all regions

		# Variable for selecting spiral type
		self.spiral_type = tk.StringVar(value="flower")  # Default to "flower"

		# Trace changes to region_uuid to update maptile_uuid accordingly
		self.region_uuid.trace_add('write', self.update_maptile_uuid)

		# Build the UI
		self.build_ui()

	def update_maptile_uuid(self, *args):
		self.maptile_uuid.set(self.region_uuid.get())

	def build_ui(self):
		canvas = tk.Canvas(self.root)
		scrollbar = tk.Scrollbar(self.root, orient="vertical", command=canvas.yview)
		scrollable_frame = tk.Frame(canvas)

		scrollable_frame.bind(
			"<Configure>",
			lambda e: canvas.configure(
			scrollregion=canvas.bbox("all")
			)
		)

		canvas.create_window((0, 0), window=scrollable_frame, anchor="nw")
		canvas.configure(yscrollcommand=scrollbar.set)

		canvas.pack(side="left", fill="both", expand=True)
		scrollbar.pack(side="right", fill="y")

		fields = [
			("Region Name:", self.region_name),
			("Region UUID:", self.region_uuid),
			("Location:", self.location),
			("Size:", self.size),
			("Internal Port:", self.internal_port),
			("External Host:", self.external_host),
			("Max Prims:", self.max_prims),
			("Max Agents:", self.max_agents),
			("Maptile UUID:", self.maptile_uuid),
			("Internal Address:", self.internal_address),
			("Allow Alternate Ports:", self.allow_alt_ports),
			("Non-Physical Prim Max:", self.non_physical_prim_max),
			("Physical Prim Max:", self.physical_prim_max),
			("Clamp Prim Size:", self.clamp_prim_size),
			("Max Prims Per User:", self.max_prims_per_user),
			("Scope ID:", self.scope_id),
			("Region Type:", self.region_type),
			("Render Min Height:", self.render_min_height),
			("Render Max Height:", self.render_max_height),
			("Maptile Static File:", self.maptile_static_file),
			("Master Avatar First Name:", self.master_avatar_first_name),
			("Master Avatar Last Name:", self.master_avatar_last_name),
			("Master Avatar Sandbox Password:", self.master_avatar_sandbox_password),
		]

		for idx, (label_text, var) in enumerate(fields):
			if isinstance(var, tk.BooleanVar):
				tk.Checkbutton(scrollable_frame, text=label_text, variable=var).grid(row=idx, column=0, columnspan=2, sticky=tk.W, pady=2, padx=5)
			else:
				tk.Label(scrollable_frame, text=label_text).grid(row=idx, column=0, sticky=tk.W, pady=2, padx=5)
				entry = tk.Entry(scrollable_frame, textvariable=var, width=40)
				entry.grid(row=idx, column=1, sticky=tk.W, pady=2, padx=5)

			# Dropdown for spiral selection
			self.spiral_type = tk.StringVar(value="flower") # Fix: Make sure the initial value of self.spiral_type is set correctly, which is already done with the line
			tk.Label(scrollable_frame, text="Spiral Type:").grid(row=len(fields), column=0, sticky=tk.W, pady=2, padx=5)
			spiral_menu = tk.OptionMenu(scrollable_frame, self.spiral_type, "archimedean_spiral1", "archimedean_spiral2", "circle1", "circle2", "fibonacci_spiral", "flower", "grid1", "grid2", "logarithmic_spiral", "logistic", "random1", "random2", "star")        
			spiral_menu.grid(row=len(fields), column=1, sticky=tk.W, pady=2, padx=5)

		button_frame = tk.Frame(scrollable_frame)
		button_frame.grid(row=len(fields) + 1, column=0, columnspan=2, pady=10)

		tk.Button(button_frame, text="Add Region", command=self.add_region).pack(side="left", padx=10)
		tk.Button(button_frame, text="Save Config", command=self.save_config).pack(side="left", padx=10)


	def add_region(self):
		# Determine which spiral to use
		location = None
		if self.spiral_type.get() == "flower":
			location = self.next_flower_spiral_location1()
		elif self.spiral_type.get() == "fibonacci_spiral":
			location = self.next_fibonacci_spiral_location()
		elif self.spiral_type.get() == "archimedean_spiral1":
			location = self.next_archimedean_spiral_location1()
		elif self.spiral_type.get() == "logarithmic_spiral":
			location = self.next_logarithmic_spiral_location()
		elif self.spiral_type.get() == "random1":
			location = self.next_random_location1()
		elif self.spiral_type.get() == "grid1":
			location = self.next_grid_location1()
		elif self.spiral_type.get() == "circle1":
			location = self.next_circle_location1()
		elif self.spiral_type.get() == "logistic":
			location = self.next_logistic_function_location()
		elif self.spiral_type.get() == "star":
			location = self.next_star_location()
		elif self.spiral_type.get() == "grid2":
			location = self.next_grid_location2()
		elif self.spiral_type.get() == "circle2":
				location = self.next_circle_location2()
		elif self.spiral_type.get() == "archimedean_spiral2":
			location = self.next_archimedean_spiral_location2()
		elif self.spiral_type.get() == "random2":
			location = self.next_random_location2()

		if location is None:
			messagebox.showerror("Error", "No spiral selected")
		return

		# Ensure location_x and location_y are not negative
		location_x, location_y = max(location[0], 0), max(location[1], 0)

		# Adjust size to one of the specified values
		valid_sizes = [256, 512, 768, 1024, 1280, 1536, 1792, 2048]
		current_size = self.size.get()
		if current_size not in valid_sizes:
			current_size = max([size for size in valid_sizes if size <= current_size], default=256)
			self.size.set(current_size)

			self.locations.append((location_x, location_y))

			new_uuid = str(uuid.uuid4())

			self.region_count += 1
			self.region_name.set(generate_random_name())
			self.region_uuid.set(new_uuid)
			self.internal_port.set(self.internal_port.get() + 1)

			self.location.set(f"{location_x},{location_y}")  # Update the location

			messagebox.showinfo("Region Added", f"Region {self.region_name.get()} added. Location: ({location_x},{location_y})")

	def save_config(self):
		filename = filedialog.asksaveasfilename(defaultextension=".ini", filetypes=[("INI files", "*.ini")])
		if not filename:return
		if filename:
			config = CaseConfigParser()  # Use the custom parser to preserve case

			for i in range(1, self.region_count + 1):
				if i == 1:
					region_name = self.region_name.get()
					region_uuid = self.region_uuid.get()
					location_x, location_y = map(int, self.location.get().split(','))  # First region
				else:
					region_name = generate_random_name()
					region_uuid = str(uuid.uuid4())
					location_x, location_y = self.locations[i-1]  # Specific location for each region

		config[region_name] = {
			"RegionUUID": region_uuid,
			"Location": f"{location_x},{location_y}",
			"SizeX": self.size.get(),
			"SizeY": self.size.get(),
			"SizeZ": self.size.get(),
			"InternalPort": self.internal_port.get() + (i - 1),
			"InternalAddress": self.internal_address.get(),
			"AllowAlternatePorts": str(self.allow_alt_ports.get()),
			"ExternalHostName": self.external_host.get(),
			"MaxPrims": self.max_prims.get(),
			"MaxAgents": self.max_agents.get(),
			"MaxPrimsPerUser": self.max_prims_per_user.get(),
			";MaptileStaticUUID": self.maptile_uuid.get(),
			";NonPhysicalPrimMax": self.non_physical_prim_max.get(),
			";PhysicalPrimMax": self.physical_prim_max.get(),
			";ClampPrimSize": str(self.clamp_prim_size.get()),
			";ScopeID": self.scope_id.get(),
			";RegionType": self.region_type.get(),
			";RenderMinHeight": self.render_min_height.get(),
			";RenderMaxHeight": self.render_max_height.get(),
			";MaptileStaticFile": self.maptile_static_file.get(),
			";MasterAvatarFirstName": self.master_avatar_first_name.get(),
			";MasterAvatarLastName": self.master_avatar_last_name.get(),
			";MasterAvatarSandboxPassword": self.master_avatar_sandbox_password.get(),
		}

		with open(filename, 'w') as configfile:
			config.write(configfile)
			messagebox.showinfo("Saved", "Configuration saved successfully.")

if __name__ == "__main__":
	root = tk.Tk()
	app = RegionConfigApp(root)
	root.mainloop()
