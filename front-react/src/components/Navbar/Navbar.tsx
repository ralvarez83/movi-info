"use client";
import React from 'react';

export type NavbarProps = {
	// types...
}

const Navbar: React.FC<NavbarProps>  = ({}) => {
	return (
		<>
		{/* <AppBar position="static">
        <Toolbar>
          <IconButton
            size="large"
            edge="start"
            color="inherit"
            aria-label="menu"
            sx={{ mr: 2 }}
          >
          </IconButton>
          <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
            Últimas películas
          </Typography> 
          <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
            Últimas películas
          </Typography> 
        </Toolbar>
				
      </AppBar> */}
			<nav>

				<h1 className="main-title">
						Movi - INFO
				</h1>
				<ul>
					<li>
						<a>
							Últimas películas
						</a>
					</li>
					<li>
						<a>
							Las más vistas
						</a>
					</li>
				</ul>
				<div className='search'>

				</div>
			</nav>
		</>
	);
};

export default Navbar;
