import { styled } from '@mui/material/styles';
import Drawer from '@mui/material/Drawer/Drawer';
import List from '@mui/material/List/List';
import Typography from '@mui/material/Typography/Typography';
import Divider from '@mui/material/Divider/Divider';
import ListItem from '@mui/material/ListItem/ListItem';
import ListItemButton from '@mui/material/ListItemButton/ListItemButton';
import ListItemText from '@mui/material/ListItemText/ListItemText';
import { useEffect } from 'react';
import { useSelector } from 'react-redux';
import { getOrganizations, getRepositories, getResources } from '../../state_management/selectors/apiSelector.ts';
import { organizationThunk, repositoryThunk, resourceThunk } from '../../state_management/slices/apiSlice.ts';
import { Organization, Repository, Resource } from '../../state_management/states/apiState.ts';
import { useAppDispatch, useAppSelector } from '../../hooks/hooks.ts';
import { Box } from '@mui/material';
import ResourceUploadButton from '../buttons/ResourceUploadButton.tsx';
import { downloadResource, fetchOrganisation, fetchOrganisationRepositories, fetchOrganisations, fetchPipeline, fetchRepositoryPipelines, fetchRepositoryResources, fetchResource, putPipeline, putRepository } from '../../services/backendAPI.tsx';
import CreateRepositoryButton from '../buttons/CreateRepositoryButton.tsx';
import AddOrganizationButton from '../buttons/AddOrganizationButton.tsx';
import { display } from 'html2canvas/dist/types/css/property-descriptors';
import OperatorUploadButton from '../buttons/OperatorUploadButton.tsx';

const drawerWidth = 240;

const DrawerHeader = styled('div')(({ theme }) => ({
  display: 'flex',
  alignItems: 'center',
  padding: theme.spacing(0, 1),
  // necessary for content to be below app bar
  ...theme.mixins.toolbar,
  justifyContent: 'flex-end',
}));

export default function PersistentDrawerLeft() {

  const dispatch = useAppDispatch()
  const organizations: Organization[] = useAppSelector(getOrganizations)
  const repositories: Repository[] = useAppSelector(getRepositories)
  const resources = useSelector(getResources)

  useEffect(() => {
    dispatch(organizationThunk())
    dispatch(repositoryThunk(organizations));
    dispatch(resourceThunk({ organizations, repositories }));

  }, [dispatch]);


  const handleDownload = async (resource: Resource) => {
    const response = await downloadResource(resource.organizationId, resource.repositoryId, resource.id) 
    await downloadReadableStream(response.url, resource.name)
  }

async function downloadReadableStream(url: string, fileName: string) {

  window.open(url, '_blank');
}

  return (
    <Drawer
      PaperProps={{
        sx: {
          backgroundColor: '#292929',
        }
      }}
      sx={{
        width: drawerWidth,
        position: 'static',
        flexGrow: 1,
        '& .MuiDrawer-paper': {
          width: drawerWidth,
          boxSizing: 'border-box',
        },
      }}
      variant="permanent"
      anchor="left"
    >
      <Divider />
      <DrawerHeader>
        <Typography sx={{ width: '100%', textAlign: 'center' }} variant="h6" noWrap component="div">
          Organisations
        </Typography>
        <AddOrganizationButton />
      </DrawerHeader>
      <List>
        {organizations.map((organization) => (
          <>
            <ListItem sx={{ justifyContent: 'center' }} key={organization.id} disablePadding>
              <p style={{marginBlock: '0rem', fontSize: '25px'}}>{organization.name}</p>
            </ListItem>
            <div style={{ display: 'flex', alignItems: 'center', paddingInline: '0.5rem' }}>
            </div>
            {repositories.map((repository) => (repository.organizationId === organization.id ?
              <>
                <ListItem key={repository.id} sx={{paddingInline: '5px'}}>
                  <p style={{padding: '0', fontSize: '25px', marginBlock: '10px'}}>{repository.name}</p>
                </ListItem>

                <div style={{ display: 'flex', alignItems: 'center', paddingInline: '0.5rem' }}>
                  <p style={{fontSize: '0.9rem' }}>Resources</p>
                  <Box sx={{ marginLeft: 'auto' }}>
                    <ResourceUploadButton orgId={repository.organizationId} repId={repository.id} />
                  </Box>
                </div>
                {resources.map((resource) => (resource.repositoryId === repository.id && resource.type !== "operator" ?
                  <>
                    <ListItem key={resource.id} disablePadding>
                      <ListItemButton sx={{ paddingBlock: 0 }} onClick={(_: any) => handleDownload(resource)}>
                        <ListItemText secondary={resource.name} secondaryTypographyProps={{ fontSize: "0.8rem" }} />
                      </ListItemButton>
                    </ListItem>
                  </> : ""
                ))}

                <div style={{ display: 'flex', alignItems: 'center', paddingInline: '0.5rem' }}>
                  <p style={{fontSize: '0.9rem'}}>Operators</p>
                  <Box sx={{ marginLeft: 'auto' }}>
                    <OperatorUploadButton orgId={repository.organizationId} repId={repository.id} />
                  </Box>
                </div>
                {resources.map((resource) => (resource.repositoryId === repository.id && resource.type === "operator" ?
                  <>
                    <ListItem key={resource.id} disablePadding>
                      <ListItemButton sx={{ paddingBlock: 0 }}>
                        <ListItemText secondary={resource.name} secondaryTypographyProps={{ fontSize: "0.8rem" }} />
                      </ListItemButton>
                    </ListItem>
                  </> : ""
                ))}
              </> : ""
            ))}
            <ListItem sx={{ justifyContent: 'center' }}>
              <Box sx={{ width: 'auto', display: 'flex', flexDirection: 'column', justifyContent: 'center', alignItems: 'center' }}>
                <CreateRepositoryButton orgId={organization.id} />
              </Box>
            </ListItem>
          </>
        ))}
      </List>
    </Drawer>
  );
}
