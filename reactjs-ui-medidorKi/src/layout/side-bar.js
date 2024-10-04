import { Inbox, Mail } from "@mui/icons-material";
import { Box, Divider, Drawer, List, ListItem, ListItemButton, ListItemIcon, ListItemText } from "@mui/material";
import React, { useState } from "react";


function SideBar() {
    const [open, setOpen] = useState(false);

    const toggleDrawer = (newOpen) => () =>{
        setOpen(newOpen);
    };

    const DrawerList = (
        <Box sx={{with:250}} role="presentation" onClick={toggleDrawer(false)}>
            <List>
                {
                    ['Resumen','Settings'].map((text, index) => (
                        <ListItem key={text} disablePadding>
                            <ListItemButton>
                                <ListItemIcon>{index % 2 === 0 ? <Inbox /> : <Mail />}</ListItemIcon>
                                <ListItemText primary={text} />
                            </ListItemButton>
                        </ListItem>
                    ))
                }
            </List>
            <Divider/>
        </Box>
    );

    return(
        <div>
            <Drawer open={true} onClose={toggleDrawer(false)} anchor="left">
                {DrawerList}
            </Drawer>
        </div>
    );
};


export default SideBar;