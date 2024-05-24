"use client";
import React from 'react';

export type NavbarProps = {
	// types...
}

export const Navbar: React.FC<NavbarProps>  = ({}) => {
	return (
		<>
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
