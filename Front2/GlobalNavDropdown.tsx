import * as React from 'react';
import {
  FormControl,
  MenuItem,
  Select,
  SelectChangeEvent,
  Typography,
  Menu,
  Collapse,
  List,
  ListItem,
  ListItemText,
  Box,
  IconButton
} from '@mui/material';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import ExpandLessIcon from '@mui/icons-material/ExpandLess';
import { useRouter } from 'next/navigation';
import { Role } from '@/app/auth/roles';
import { NavArea, NavTab } from '../../types/navigation';

interface GlobalNavDropdownProps {
  area: NavArea;
  index: number;
  userRoles: Role[];
}

const GlobalNavDropdown: React.FC<GlobalNavDropdownProps> = ({
  area,
  userRoles
}) => {
  const router = useRouter();
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const [expandedItems, setExpandedItems] = React.useState<Set<string>>(new Set());

  const handleClick = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
    setExpandedItems(new Set());
  };

  const handleItemClick = (path: string) => {
    router.push(path);
    handleClose();
  };

  const toggleExpanded = (itemLabel: string) => {
    const newExpanded = new Set(expandedItems);
    if (newExpanded.has(itemLabel)) {
      newExpanded.delete(itemLabel);
    } else {
      newExpanded.add(itemLabel);
    }
    setExpandedItems(newExpanded);
  };

  const hasAccess = (requiredRoles?: Role[], requireAll = false): boolean => {
    if (!requiredRoles || requiredRoles.length === 0) return true;
    return requireAll
      ? requiredRoles.every((r) => userRoles.includes(r))
      : requiredRoles.some((r) => userRoles.includes(r));
  };

  const renderNestedItems = (tabs: NavTab[], level: number = 0) => {
    return tabs
      .filter((tab) => hasAccess(tab.roles, tab.requireAll))
      .map((tab, idx) => (
        <React.Fragment key={`${level}-${idx}`}>
          <ListItem
            sx={{
              pl: 2 + level * 2,
              py: 0.5,
              cursor: 'pointer',
              '&:hover': { backgroundColor: 'rgba(0, 0, 0, 0.04)' }
            }}
          >
            <ListItemText
              primary={tab.label}
              onClick={() => tab.tabs ? toggleExpanded(tab.label) : handleItemClick(tab.path)}
              sx={{
                '& .MuiListItemText-primary': {
                  fontSize: '0.875rem',
                  fontWeight: tab.tabs ? 'bold' : 'normal'
                }
              }}
            />
            {tab.tabs && (
              <IconButton
                size="small"
                onClick={() => toggleExpanded(tab.label)}
                sx={{ p: 0.5 }}
              >
                {expandedItems.has(tab.label) ? <ExpandLessIcon /> : <ExpandMoreIcon />}
              </IconButton>
            )}
          </ListItem>
          {tab.tabs && (
            <Collapse in={expandedItems.has(tab.label)} timeout="auto" unmountOnExit>
              <List component="div" disablePadding>
                {renderNestedItems(tab.tabs, level + 1)}
              </List>
            </Collapse>
          )}
        </React.Fragment>
      ));
  };

  return (
    <>
      <Box
        onClick={handleClick}
        sx={{
          cursor: 'pointer',
          display: 'flex',
          alignItems: 'center',
          color: 'white',
          '&:hover': { opacity: 0.8 }
        }}
      >
        <Typography
          sx={{
            fontSize: '0.875rem',
            fontWeight: 500
          }}
        >
          {area.label}
        </Typography>
        <ExpandMoreIcon sx={{ ml: 0.5, fontSize: '1rem' }} />
      </Box>
      <Menu
        anchorEl={anchorEl}
        open={Boolean(anchorEl)}
        onClose={handleClose}
        anchorOrigin={{
          vertical: 'bottom',
          horizontal: 'left',
        }}
        transformOrigin={{
          vertical: 'top',
          horizontal: 'left',
        }}
        PaperProps={{
          sx: {
            minWidth: 250,
            maxHeight: 400,
            overflow: 'auto'
          }
        }}
      >
        <List dense>
          {area.tabs && renderNestedItems(area.tabs)}
        </List>
      </Menu>
    </>
  );
};

export default GlobalNavDropdown;
